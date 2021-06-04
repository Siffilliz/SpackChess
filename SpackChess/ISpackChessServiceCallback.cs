using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SpackChess
{
    // HINWEIS: Mit dem Befehl "Umbenennen" im Menü "Umgestalten" können Sie den Schnittstellennamen "ISpackChessServiceCallback" sowohl im Code als auch in der Konfigurationsdatei ändern.
    [ServiceContract]
    public interface ISpackChessServiceCallback
    {
        [OperationContract]
        void DoWork();
    }
}
