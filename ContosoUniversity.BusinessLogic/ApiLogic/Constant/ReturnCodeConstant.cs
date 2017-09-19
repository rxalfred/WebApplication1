using System;
using System.Collections.Generic;
using System.Text;

namespace ContosoUniversity.BusinessLogic.ApiLogic.Constant
{
    public static class ReturnCodeConstant
    {
        public static readonly ReturnCodeObj SUCCESS = new ReturnCodeObj { Code = ReturnCodeEnum.SUCCESS.ToInt(), Message = "Success" };
        public static readonly ReturnCodeObj SUCCESS_BUT_FAILED_TO_RECORD_TRX = new ReturnCodeObj { Code = ReturnCodeEnum.SUCCESS_BUT_FAILED_TO_RECORD_TRX.ToInt(), Message = "Transaction successful but failed to record. Please contact our customer service with your reserve reference number." };

        public static readonly ReturnCodeObj ERR_SYSTEM_ERROR = new ReturnCodeObj { Code = ReturnCodeEnum.SYSTEM_ERROR.ToInt(), Message = "System error." };
        public static readonly ReturnCodeObj ERR_INVALID_INPUT = new ReturnCodeObj { Code = ReturnCodeEnum.INPUT_ERROR.ToInt(), Message = "Invalid input." };
        public static readonly ReturnCodeObj ERR_INVALID_CURRENCY = new ReturnCodeObj { Code = ReturnCodeEnum.INPUT_ERROR.ToInt(), Message = "Currency is not supported." };
        public static readonly ReturnCodeObj ERR_INVALID_SIGNATURE = new ReturnCodeObj { Code = ReturnCodeEnum.INVALID_SIGNATURE.ToInt(), Message = "Invalid signature." };

        public static class COMMON
        {
            public static readonly ReturnCodeObj ERR_INVALID_BOOKING_REF_NO = new ReturnCodeObj { Code = ReturnCodeEnum.INVALID_BOOKING_REF_NO.ToInt(), Message = "invalid booking reference no." };
            public static readonly ReturnCodeObj ERR_INVALID_CURRENCY_CONVERSION_PAIR = new ReturnCodeObj { Code = ReturnCodeEnum.INVALID_CURRENCY_CONVERSION_PAIR.ToInt(), Message = "invalid currency conversion pair." };
            public static readonly ReturnCodeObj ERR_USER_NOT_FOUND = new ReturnCodeObj { Code = ReturnCodeEnum.USER_NOT_FOUND.ToInt(), Message = "user id not found." };

            #region Internal api common return code
            public static readonly ReturnCodeObj ERR_INVALID_PAYMENTGATEWAY = new ReturnCodeObj { Code = ReturnCodeEnum.INVALID_PAYMENTGATEWAY.ToInt(), Message = "Cannot check out using this payment option. Please try again later or choose other payment option." };
            #endregion Internal api common return code
        }
    }

    public enum ReturnCodeEnum
    {
        SUCCESS = 1,
        SUCCESS_BUT_FAILED_TO_RECORD_TRX = 2,

        BUS_QUERYFARE_FAIL = -1001,
        BUS_RESERVESEAT_FAIL = -1002,
        BUS_RESERVESEAT_TRIPEXPIRED = -1003,
        BUS_CONFIRMSEAT_FAIL = -1004,
        BUS_QUERYSEAT_FAIL = -1005,
        BUS_INVALID_REQUEST = -1006,

        TRAIN_INVALID_TRIPKEY = -5001,
        TRAIN_COACH_NOT_FOUND = -5002,
        TRAIN_INVALID_COMPANYID = -5003,
        TRAIN_MAX_PAX_EXCEED = -5004,
        TRAIN_QUERYFARE_FAIL = -5005,
        TRAIN_RESERVESEAT_FAIL = -5006,
        TRAIN_RESERVESEAT_TRIPEXPIRED = -5007,
        TRAIN_CONFIRMSEAT_FAIL = -5008,
        TRAIN_RESERVESEAT_SEATLOCKED = -5009,

        SYSTEM_ERROR = -9001,
        INPUT_ERROR = -9002,
        ERR_INVALID_CURRENCY = -9003,
        INVALID_BOOKING_REF_NO = -9004,
        INVALID_CURRENCY_CONVERSION_PAIR = -9005,
        USER_NOT_FOUND = -9006,
        INVALID_SIGNATURE = -9007,
        INVALID_PAYMENTGATEWAY = -9008,

        FERRY_QUERYFARE_FAIL = -2001,
        FERRY_RESERVESEAT_FAIL = -2002,
        FERRY_RESERVESEAT_TRIPEXPIRED = -2003,
        FERRY_CONFIRMSEAT_FAIL = -2004,
        FERRY_QUERYSEAT_FAIL = -2005
    }

    public class ReturnCodeObj
    {
        public int Code;
        public string Message;
    }

    public static class ApiReturnCodeEnumExtension
    {
        public static int ToInt(this ReturnCodeEnum returnCode)
        {
            return (int)returnCode;
        }

        public static string ToUpperString(this ReturnCodeEnum returnCode)
        {
            return returnCode.ToString().ToUpper();
        }

        public static string ToLowerString(this ReturnCodeEnum returnCode)
        {
            return returnCode.ToString().ToLower();
        }
    }
}
