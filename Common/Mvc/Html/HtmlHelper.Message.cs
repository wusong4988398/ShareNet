using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace ShareNet.Common
{
    /// <summary>
    /// 扩展对Message的HtmlHelper使用方法
    /// </summary>
    public static class HtmlHelperMessageExtensions
    {
        public static MvcHtmlString PresetMessage(this HtmlHelper htmlHelper, string messageContent, bool isHighlight)
        {
            htmlHelper.ViewData["IsHighlight"] = isHighlight;
            htmlHelper.ViewData["MessageContent"] = messageContent;
            return htmlHelper.DisplayForModel("PresetMessage");
        }
    }

    /// <summary>
    /// 辅助传输StatusMessage数据
    /// </summary>
    [Serializable]
    public sealed class StatusMessageData
    {
        private StatusMessageType messageType;
        /// <summary>
        /// 提示消息类别
        /// </summary>
        public StatusMessageType MessageType
        {
            get { return messageType; }
            set { messageType = value; }
        }

        private string messageContent = string.Empty;
        /// <summary>
        /// 信息内容
        /// </summary>
        public string MessageContent
        {
            get { return messageContent; }
            set { messageContent = value; }
        }
        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="messageType">消息类型</param>
        /// <param name="messageContent">消息内容</param>
        public StatusMessageData(StatusMessageType messageType, string messageContent)
        {
            this.messageType = messageType;
            this.messageContent = messageContent;
        }
    }


    /// <summary>
    /// 提示消息类别
    /// </summary>
    public enum StatusMessageType
    {
        /// <summary>
        /// 成功
        /// </summary>
        Success = 1,

        /// <summary>
        /// 错误
        /// </summary>
        Error = -1,

        /// <summary>
        /// 提示信息
        /// </summary>
        Hint = 0
    }
}
