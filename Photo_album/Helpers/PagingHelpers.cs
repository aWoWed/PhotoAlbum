using System;
using System.Text;
using System.Web.Mvc;
using Photo_album.Models;

namespace Photo_album.Helpers
{
    /// <summary>
    ///     Represents paging helper for paging
    /// </summary>
    public static class PagingHelpers
    {
        /// <summary>
        ///     Creates paging button menu
        /// </summary>
        /// <param name="html"></param>
        /// <param name="pageInfo"></param>
        /// <param name="pageUrl"></param>
        /// <returns>Paging button menu</returns>
        public static MvcHtmlString PageLinks(this HtmlHelper html, PageInfo pageInfo, Func<int, string> pageUrl)
        {
            var result = new StringBuilder();

            for (var i = 1; i <= pageInfo.TotalPages; i++)
            {
                var tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString();

                if (i == pageInfo.PageNumber)
                {
                    tag.AddCssClass("selected");
                    tag.AddCssClass("btn-primary");
                }

                tag.AddCssClass("btn btn-default");

                result.Append(tag);
            }

            return MvcHtmlString.Create(result.ToString());
        }
    }
}
