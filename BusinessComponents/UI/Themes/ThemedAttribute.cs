using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using ShareNet.Common.PresentAreas;

namespace ShareNet.Common.UI.Themes
{
    /// <summary>
    /// 用于controller的Theme相关属性标注
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ThemedAttribute:ActionFilterAttribute
    {

        public ThemedAttribute(string presentAreaKey)
        {
            this.PresentAreaKey = presentAreaKey;
        }
        /// <summary>
        /// 呈现区域标识
        /// </summary>        
        public string PresentAreaKey { get; private set; }

        /// <summary>
        /// 是否属于应用模块
        /// </summary>
        public bool IsApplication { get; set; }

        private PresentAreaService presentAreaService = null;
        /// <summary>
        /// PresentAreaService
        /// </summary>
        protected virtual PresentAreaService PresentAreaService
        {
            get
            {
                if (presentAreaService == null)
                    presentAreaService = new PresentAreaService();

                return presentAreaService;
            }
            set { presentAreaService = value; }
        }


    }
}
