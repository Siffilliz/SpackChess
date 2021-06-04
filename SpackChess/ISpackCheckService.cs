using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SpackChess
{
    // HINWEIS: Mit dem Befehl "Umbenennen" im Menü "Umgestalten" können Sie den Schnittstellennamen "IISpackCheckService" sowohl im Code als auch in der Konfigurationsdatei ändern.
    [ServiceContract(CallbackContract =typeof(ISpackCheckService))]
    public interface ISpackCheckService
    {
        [OperationContract]
        void DoWork();
    }
}
