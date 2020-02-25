using SMSGateway.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SMS.Models
{
    public class SMSContext : DbContext
    {
        public DbSet<TextMessage> texts { get; set; }
    }
}