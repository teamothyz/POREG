namespace POREG.Models
{
    public class POInfor
    {
        public int ID { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Birthday { get; set; } = string.Empty;
        public string IDCard { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string BankNumber { get; set; } = string.Empty;
        public string BankName { get; set; } = string.Empty;
        public int BankCode { get; set; } = 0;
        public string Status { get; set; } = "NotStart";
        private string SandNumber { get; set; } = string.Empty;

        public POInfor()
        {
            var random = new Random();
            BankCode = random.Next(0, 7);

            SandNumber = string.Empty;
            var count = random.Next(1, 5);
            for (var i = 0; i < count; i++)
            {
                var nextNumber = random.Next(0, 9);
                SandNumber += nextNumber;
            }
        }

        public string GetFirstName()
        {
            var lastIndex = Name.LastIndexOf(" ");
            if (lastIndex != -1)
            {
                var firstname = Name[lastIndex..];
                return firstname.Trim();
            }
            else
            {
                return Name;
            }
        }

        public string GetLastName()
        {
            var lastIndex = Name.LastIndexOf(" ");
            if (lastIndex != -1)
            {
                var lastname = Name[..lastIndex];
                return lastname.Trim();
            }
            else
            {
                return Name;
            }
        }

        public string GetStreetAddress()
        {
            var lastIndex = Address.LastIndexOf(", ");
            if (lastIndex != -1)
            {
                var street = Address[..lastIndex];
                return street.Trim();
            }
            else
            {
                throw new FormatException("Incorrect Address Format.");
            }
        }

        public string GetCity()
        {
            var lastIndex = Address.LastIndexOf(", ");
            if (lastIndex != -1)
            {
                var city = Address[lastIndex..].Replace(",", "");
                return city.Trim();
            }
            else
            {
                throw new FormatException("Incorrect Address Format.");
            }
        }

        public string GetBankNumber()
        {
            BankNumber = Phone + SandNumber;
            return BankNumber;
        }

        public string GetBankCode()
        {
            return ((SWIFTCODE)BankCode).ToString();
        }

        public string GetDayOfBirth()
        {
            try
            {
                var data = Birthday.Split('/');
                return data[0];
            }
            catch (Exception)
            {
                throw new FormatException("Incorrect Birth Date Format.");
            }
        }

        public string GetMonthOfBirth()
        {
            try
            {
                var data = Birthday.Split('/');
                return data[1];
            }
            catch (Exception)
            {
                throw new FormatException("Incorrect Birth Date Format.");
            }
        }

        public string GetYearOfBirth()
        {
            try
            {
                var data = Birthday.Split('/');
                return data[2];
            }
            catch (Exception)
            {
                throw new FormatException("Incorrect Birth Date Format.");
            }
        }

        public string GetBankBranchName()
        {
            switch ((SWIFTCODE)BankCode)
            {
                case SWIFTCODE.TPBVVNVX:
                    BankName = "TP Bank";
                    return "TIENPHONG";
                case SWIFTCODE.VPBKVNVX:
                    BankName = "VP Bank";
                    return "PROSPERITY JOINT STOCK COMMERCIAL";
                case SWIFTCODE.BIDVVNVX:
                    BankName = "BIDV";
                    return "JOINT STOCK COMMERCIAL BANK FOR INVESTMENT AND DEVELOPMENT";
                case SWIFTCODE.VTCBVNVX:
                    BankName = "Techcombank";
                    return "TECHNOLOGICAL AND COMMERCIAL JOINT STOCK";
                case SWIFTCODE.ASCBVNVX:
                    BankName = "ACB";
                    return "ACB SECURITIES COMPANY";
                case SWIFTCODE.BFTVVNVX:
                    BankName = "Vietcombank";
                    return "JOINT STOCK COMMERCIAL BANK FOR FOREIGN TRADE";
                case SWIFTCODE.ICBVVNVX:
                    BankName = "Vietinbank";
                    return "JOINT STOCK COMMERCIAL BANK FOR INDUSTRY AND TRADE";
                case SWIFTCODE.MSCBVNVX:
                    BankName = "MB";
                    return "MILITARY COMMERCIAL JOINT STOCK";
                default:
                    BankName = "TP Bank";
                    return "TIENPHONG";
            }
        }
    }

    public enum SWIFTCODE
    {
        TPBVVNVX = 0,   //TPBank
        VPBKVNVX = 1,   //VPBank
        BIDVVNVX = 2,   //BIDV
        VTCBVNVX = 3,   //TechcomBank
        ASCBVNVX = 4,   //ACB
        BFTVVNVX = 5,   //VietcomBank
        ICBVVNVX = 6,   //VietinBank
        MSCBVNVX = 7,   //MB
    }
}
