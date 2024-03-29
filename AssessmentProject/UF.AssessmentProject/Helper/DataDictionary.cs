﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UF.AssessmentProject.Helper
{
    public class DataDictionary
    {
        /// <summary>
        /// response Result
        ///    failed = 0,
        ///    success = 1,
        ///    pending = 2
        /// </summary>
        public enum responseResult
        {
            /// <summary>
            /// Failed
            /// </summary> 
            failed = 0,
            success = 1 
        }

        #region Partner Auth
        public enum partnerkey
        {
            FAKEGOOGLE,
            FAKEPEOPLE
        }

        public enum partnerpassword
        {
            FAKEPASSWORD1234,
            FAKEPASSWORD4578
        }

        #endregion
    }
}
