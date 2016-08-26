using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WusNet.Infrastructure.Events
{
    public class EventOperationType
    {
        private static volatile EventOperationType _instance = null;
        private static readonly object lockObject = new object();
        public static EventOperationType Instance()
        {
            if (_instance == null)
            {
                lock (lockObject)
                {
                    if (_instance == null)
                    {
                        _instance = new EventOperationType();
                    }
                }
            }
            return _instance;
        }

        private EventOperationType()
        {
        }


        /// <summary>
        /// 通过审核
        /// </summary>
        /// <returns></returns>
        public string Approved()
        {
            return "Approved";
        }
        /// <summary>
        /// 取消精华
        /// </summary>
        /// <returns></returns>
        public string CancelEssential()
        {
            return "CancelEssential";
        }
        /// <summary>
        /// 取消置顶
        /// </summary>
        /// <returns></returns>
        public string CancelSticky()
        {
            return "CancelSticky";
        }
        /// <summary>
        /// 受控查看
        /// </summary>
        /// <returns></returns>
        public string ControlledView()
        {
            return "ControlledView";
        }
        /// <summary>
        ///  创建
        /// </summary>
        /// <returns></returns>
        public string Create()
        {
            return "Create";
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        public string Delete()
        {
            return "Delete";
        }
        /// <summary>
        /// 不通过审核
        /// </summary>
        /// <returns></returns>
        public string Disapproved()
        {
            return "Disapproved";
        }
        /// <summary>
        /// 设置分类
        /// </summary>
        /// <returns></returns>
        public string SetCategory()
        {
            return "SetCategory";
        }
        /// <summary>
        /// 设置精华
        /// </summary>
        /// <returns></returns>
        public string SetEssential()
        {
            return "SetEssential";
        }
        /// <summary>
        /// 设置置顶
        /// </summary>
        /// <returns></returns>
        public string SetSticky()
        {
            return "SetSticky";
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <returns></returns>
        public string Update()
        {
            return "Update";
        }





    }
}
