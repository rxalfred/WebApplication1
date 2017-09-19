using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ContosoUniversity.BusinessLogic.ApiLogic.BusinessObjects
{
    public partial class Response
    {
        /// <summary>
        /// 1: PASSED 
        /// 0: FAILED
        /// </summary>
        [Required]
        public int Status { get; set; } = 0;

        /// <summary>
        /// RETURN CODE
        /// Refer to Documentation for Return Code
        /// </summary>
        [Required]
        public int Code { get; set; } = 0;

        /// <summary>
        /// RETURN MESSAGE
        /// Refer to Documentation for Return Code Message
        /// </summary>
        [Required]
        public string Message { get; set; } = "";
    }
}