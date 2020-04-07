using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Users.Queries.GetAll
{
    public class UserVMPagination
    {

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int ItemsPerPage { get; set; }
        public int TotalItems { get; set; }

        
        public UserVMPagination(int currentPage, int totalPages, int itemsPerPage, int totalItems, IEnumerable<UserVm> users)
        {
            CurrentPage = currentPage;
            TotalPages = totalPages;
            ItemsPerPage = itemsPerPage;
            TotalItems = totalItems;
            Users = users;
        }
        public IEnumerable<UserVm> Users { get; set; }

    }
}
