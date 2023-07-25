﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace testAPI.Models
{
    public partial class Member
    {
        public Member()
        {
            Cart = new HashSet<Cart>();
            CustomerService = new HashSet<CustomerService>();
            PointHistory = new HashSet<PointHistory>();
            ProductOrder = new HashSet<ProductOrder>();
            TarotOrder = new HashSet<TarotOrder>();
        }

        public int MemberId { get; set; }
        public string MemberName { get; set; }
        public string MemberTel { get; set; }
        public string MemberAddress { get; set; }
        public string MemberEmail { get; set; }
        public DateTime MemberBirthday { get; set; }
        public bool? MemberSex { get; set; }
        public int? MemberPoint { get; set; }
        public DateTime MemberJoinTime { get; set; }
        public bool? Available { get; set; }

        public virtual MemberCredential MemberCredential { get; set; }
        public virtual ICollection<Cart> Cart { get; set; }
        public virtual ICollection<CustomerService> CustomerService { get; set; }
        public virtual ICollection<PointHistory> PointHistory { get; set; }
        public virtual ICollection<ProductOrder> ProductOrder { get; set; }
        public virtual ICollection<TarotOrder> TarotOrder { get; set; }
    }
}