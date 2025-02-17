﻿using Blogging.Common.Domain;
using Blogging.Modules.Blog.Domain.Blogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Modules.Blog.Domain.Users
{
    public sealed class User : Entity
    {
        private readonly HashSet<Domain.Blogs.Blog> _contributedblogs = []; 
        public Guid Id { get; private set ; }
        public string UserName { get; private set; }
        public string DisplayName { get; private set; }
        public string Email { get; private set; }
        public string ImageUrl { get; private set; }
        public IReadOnlyCollection<Blogs.Blog> ContributedBlogs => _contributedblogs;

        public static User Create(Guid Id
              , string UserName
              , string DisplayName
              , string Email
              , string ImageUrl)
        {
            User users = new User
            {
                Id = Id,
                UserName = UserName,
                DisplayName = DisplayName,
                Email = Email,
                ImageUrl = ImageUrl,
            };
            return users;
        }
        public void Update(string userName
            , string displayName
            , string email
            , string imageUrl)
        {
            UserName = userName;
            DisplayName = displayName;
            Email = email;
            ImageUrl = imageUrl;
        }
    }
}
