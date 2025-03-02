using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace QuanLyThuVien
{
    public static class Parameters
    {
        public static bool flagLoadLoanSlipDetail = false;

        public static int nAccount = 0;
        public static int nRole = 0;
        public static int nAccountRole = 0;
        public static int nReader = 0;

        public static decimal RentalPrice = 10m;
        public static decimal DepositRercentage = 0.5m;

        public static string PenaltyReason;
    }
}