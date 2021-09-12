using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Photo_album.Helpers
{
    /// <summary>
    ///     Represents html helpers
    /// </summary>
    public static class HtmlHelpers
    {
        /// <summary>
        ///     Creates checkbox
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="model"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns>Checkbox</returns>
        public static MvcHtmlString MyCheckboxFor<T1>(this HtmlHelper<T1> htmlHelper, Expression<Func<T1, bool>> model,
            object htmlAttributes)
        {
            var checkBoxWithHidden = htmlHelper.CheckBoxFor(model, htmlAttributes).ToHtmlString().Trim();
            var pureCheckBox = checkBoxWithHidden.Substring(0, checkBoxWithHidden.IndexOf("<input", 1, StringComparison.Ordinal));
            return new MvcHtmlString(pureCheckBox);
        }
    }
}
