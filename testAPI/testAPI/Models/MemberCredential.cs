﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace testAPI.Models
{
    public partial class MemberCredential
    {
        public int MemberId { get; set; }
        public string MemberAccount { get; set; }
        public string MemberPassword { get; set; }

        public virtual Member Member { get; set; }
    }
}