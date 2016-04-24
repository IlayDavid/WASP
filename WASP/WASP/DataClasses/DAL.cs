﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WASP.DataClasses
{
    public interface DAL
    {

        //forum.Id == -1
         Forum CreateForum(Forum forum);
        //forum.Id > -1
         Forum UpdateForum(Forum forum);
        //forumsId == null -> get all forums. else get all forums in the collection.
         Forum[] GetForums(Collection<int> forumsIds);

        //subForum.id == -1
         Subforum CreateSubForum(Subforum sf);
        //subForum.id > -1
         Subforum UpdateSubForum(Subforum sf);
        //subForumIds == null -> get all subForums. else, get all subForums in the forum.
         Subforum[] GetSubForums(Collection<int> subForumIds, Forum forum);

        //user.id == -1
         User CreateUser(User user);
        //user.id >-1
         User updateUser(User user);
        //userIds == null -> get all users. else get all users in the forum
         User[] GetUseres(Collection<int> userIds, Forum forum);


        //moderator.id == -1
         Moderator CreateModerator(Moderator mod);
        //moderator.id >-1
         Moderator updateModerator(Moderator mod);
        //moderatorIds == null -> get all mods. else get all mods in the forum
         Moderator[] GetModerators(Collection<int> moderatorIds, Forum forum);


        //Admins.id == -1
         Admin CreateAdmin(Admin admin);
        //Admins.id >-1
         Admin UpdateAdmin(Admin admin);
        //AdminIds == null -> get all admins
         Admin[] GetAdmins(Collection<int> adminsIds);










    }
}
