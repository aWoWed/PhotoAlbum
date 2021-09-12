using System;

namespace Photo_album.Models
{
    /// <summary>
    ///     Represents model for paging
    /// </summary>
    public class PageInfo
    {
        /// <summary>
        ///     Gets or sets number of the first page
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        ///     Gets or sets page size
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        ///     Gets or sets items on page
        /// </summary>
        public int TotalItems { get; set; }

        /// <summary>
        ///      Gets or sets number of pages
        /// </summary>
        public int TotalPages => (int)Math.Ceiling((decimal)TotalItems / PageSize);
    }

}
