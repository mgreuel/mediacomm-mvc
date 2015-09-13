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

        public static IDocumentSession CurrentSession => HttpContext.Current.Items["RavenDbSession"] as IDocumentSession ??
                                                         ((IDocumentSession)
                                                          (HttpContext.Current.Items["RavenDbSession"] = documentStore.OpenSession()));

        public static IDocumentStore Initialize()
        {
            documentStore = new DocumentStore { ConnectionStringName = "raven" };
            
            // replacing / with - as a separator allows using the ids in urls
            documentStore.Conventions.IdentityPartsSeparator = "-";
            documentStore.Initialize();

            return documentStore;
        }
    }
}