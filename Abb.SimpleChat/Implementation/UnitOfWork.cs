using System;
using System.Collections.Generic;
using System.Text;
using Abb.SimpleChat.Business.Logic.Entities;

namespace Abb.SimpleChat.External.Implementation
{
    public class UnitOfWork
    {
        private EntityContext context = new EntityContext();
        private RepositoryImplementation<User> userRepository;
        private RepositoryImplementation<Message> messageRepository;
        private RepositoryImplementation<Role> roleRepository;
        private RepositoryImplementation<Log> logRepository;

        public RepositoryImplementation<User> UserRepository
        {
            get
            {

                if (this.userRepository == null)
                {
                    this.userRepository = new RepositoryImplementation<User>(context);
                }
                return userRepository;
            }
        }

        public RepositoryImplementation<Message> MessageRepository
        {
            get
            {

                if (this.messageRepository == null)
                {
                    this.messageRepository = new RepositoryImplementation<Message>(context);
                }
                return messageRepository;
            }
        }
        public RepositoryImplementation<Role> RoleRepository
        {
            get
            {

                if (this.roleRepository == null)
                {
                    this.roleRepository = new RepositoryImplementation<Role>(context);
                }
                return roleRepository;
            }
        }
        public RepositoryImplementation<Log> LogRepository
        {
            get
            {

                if (this.logRepository == null)
                {
                    this.logRepository = new RepositoryImplementation<Log>(context);
                }
                return logRepository;
            }
        }


        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
