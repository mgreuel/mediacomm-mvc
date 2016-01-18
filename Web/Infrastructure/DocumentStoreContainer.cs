using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Raven.Client;
using Raven.Client.Document;

namespace MediaCommMvc.Web.Infrastructure
{
    public static class DocumentStoreContainer
    {
        private static DocumentStore documentStore;

        public static IDocumentSession CurrentRequestSession => HttpContext.Current.Items["RavenDbSession"] as IDocumentSession ??
                                                         ((IDocumentSession)
                                                          (HttpContext.Current.Items["RavenDbSession"] = documentStore.OpenSession()));

        public static IDocumentSession NewSession => documentStore.OpenSession();

        public static IDocumentStore Initialize()
        {
            documentStore = new DocumentStore { ConnectionStringName = "raven" };
            
            // replacing / with - as a separator allows using the ids in urls
            documentStore.Conventions.IdentityPartsSeparator = "-";

            // allows us to sort by enum value
            documentStore.Conventions.SaveEnumsAsIntegers = true;

            documentStore.Initialize();
            
            return documentStore;
        }
    }
}