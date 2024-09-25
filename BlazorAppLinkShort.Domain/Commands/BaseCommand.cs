using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUpload.Domain.Commands
{
    public interface ICommand { bool Validate(); }

    public abstract class BaseCommand : Notifiable<Notification>, ICommand
    {
        public virtual bool Validate()
        {
            AddNotifications(new Contract<BaseCommand>().Requires()
            );

            return IsValid;
        }
    }
}
