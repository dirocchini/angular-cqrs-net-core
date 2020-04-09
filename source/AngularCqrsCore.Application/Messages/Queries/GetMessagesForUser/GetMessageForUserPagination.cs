using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Messages.Queries.GetMessagesForUser
{
    public class GetMessageForUserPagination
    {

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int ItemsPerPage { get; set; }
        public int TotalItems { get; set; }


        public GetMessageForUserPagination(int currentPage, int totalPages, int itemsPerPage, int totalItems, IEnumerable<GetMessagesForUserDto> messages)
        {
            CurrentPage = currentPage;
            TotalPages = totalPages;
            ItemsPerPage = itemsPerPage;
            TotalItems = totalItems;
            Messages = messages;
        }
        public IEnumerable<GetMessagesForUserDto> Messages { get; set; }
    }
}
