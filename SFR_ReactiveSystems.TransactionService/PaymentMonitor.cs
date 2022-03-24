using StrawberryShake;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFR_ReactiveSystems.TransactionService
{
    public class PaymentMonitor : IObserver<IOperationResult<IOnNewPayment_Payments>>
    {
        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(IOperationResult<IOnNewPayment_Payments> value)
        {
            throw new NotImplementedException();
        }
    }
}
