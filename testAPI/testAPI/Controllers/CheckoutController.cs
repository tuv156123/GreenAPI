using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using testAPI.Models;

namespace testAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckoutController : ControllerBase
    {
        private readonly AzureChilLaxDbContext _context;
        public CheckoutController(AzureChilLaxDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 測試中
        /// </summary>
        /// <example>jskjdksdkjskj</example>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdatePaymentAsync")]
        public async Task<ActionResult<string>> UpdatePaymentAsync([FromBody] GreenPayResult payResult)
        {
            if (payResult == null || _context.ProductOrder == null || payResult.RtnCode != 1) return "付款失敗";
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                string ID = payResult.MerchantTradeNo.Replace("ChilLax", "");
                int orderID = Convert.ToInt32(ID);

                var thisOrder = await _context.ProductOrder.FirstOrDefaultAsync(po => po.OrderId == orderID);

                ProductOrder productOrder = thisOrder;
                if (productOrder == null)
                    return "找不到該訂單";

                //修改付款狀態
                productOrder.OrderPayment = true;
                _context.ProductOrder.Update(productOrder);
                await _context.SaveChangesAsync();

                //新增點數回饋
                PointHistory pointHistory = new PointHistory();
                pointHistory.ModifiedSource = "product";
                pointHistory.MemberId = productOrder.MemberId;
                pointHistory.PointDetailId = productOrder.OrderId.ToString();
                pointHistory.ModifiedAmount = (int)Math.Floor(productOrder.OrderTotalPrice / 10.0);
                _context.PointHistory.Add(pointHistory);
                await _context.SaveChangesAsync();

                // 提交交易
                await transaction.CommitAsync();

                return "1|OK";
            }
            catch (Exception ex)
            {
                // 發生例外時回滾交易
                await transaction.RollbackAsync();
                return "付款失敗：" + ex.Message;
            }
        }
    }
}
