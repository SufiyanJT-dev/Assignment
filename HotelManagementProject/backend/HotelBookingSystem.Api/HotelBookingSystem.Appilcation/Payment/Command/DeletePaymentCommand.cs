using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem.Appilcation.Payment.Command
{
    public class DeletePaymentCommand:IRequest<string>
    {
        public int Id { get; set; }
    }
}
