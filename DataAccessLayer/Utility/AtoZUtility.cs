using DataAccessLayer.DTO.HouseKeeping;
namespace DataAccessLayer.Utility
{
    public class AtoZUtility
    {
        public string GeneratePassword(string inData, int nFlag)
        {
            return SoftValidity.SoftValidity.DataEncryptDecrypt(inData, nFlag);

            
        }

        public int CheckAtoZLicense()
        {
            A2ZERPSYSPRMDTO prmDto = A2ZERPSYSPRMDTO.GetParameterValue();

            string strValue = SoftValidity.SoftValidity.CheckAtoZLicence(@"D:\A2ZMCUS\A2ZERPPRM.DAT");            

            if (strValue.Substring(0, 14) == "File Not Found")
            {
                return 1;
            }
            if (strValue != prmDto.PrmUnitName)
            {
                return 2;
            }
            return 0;
        }

        public int CheckClientRegistry()
        {
            return SoftValidity.SoftValidity.CheckSoftValidity();
        }

        public string ShowRegistryValue()
        {
            return SoftValidity.SoftValidity.ShowSoftValidify();
        }

        public int UpdateClientRegistry(string keyValue)
        {
            return SoftValidity.SoftValidity.RegistrySoftValidity(keyValue);
        }

        public static string ConvertTextArray(int nMonth, string ConvertValue, string ExactValue, int nLength, int nSize, int nTimes, int nFill)
        {
           return SoftValidity.SoftValidity.ConvertTextArray(nMonth,  ConvertValue,  ExactValue,  nLength,  nSize,  nTimes,  nFill);
        }

    }
}
