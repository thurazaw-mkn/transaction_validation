 
using System.ComponentModel.DataAnnotations; 
using UF.AssessmentProject.Helper;

namespace UF.AssessmentProject.Model
{
    public class ResponseMessage
    {
        /// <summary>
        ///  Response Result
        /// </summary>
        /// <example>1</example>
        [Required]
        public DataDictionary.responseResult result { get; set; }
        //public DataDictionary.responseResult success { get; set; } // i think that naming may be unstable

        /// <summary>
        /// Error Messages Description
        /// </summary>
        /// <example>Success</example>
        [Required]
        public string errormessage { get; set; }

    }
}
