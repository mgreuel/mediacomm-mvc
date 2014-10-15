﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Mail {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Mail() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("MediaCommMvc.Web.Resources.Mail", typeof(Mail).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} uploaded new photos in the album &apos;{1}&apos;.
        /// </summary>
        public static string NewPhotosBody {
            get {
                return ResourceManager.GetString("NewPhotosBody", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to New photos on .
        /// </summary>
        public static string NewPhotosTitle {
            get {
                return ResourceManager.GetString("NewPhotosTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} replied to the topic &apos;{1}&apos; on {2}.
        /// </summary>
        public static string NewPostBody {
            get {
                return ResourceManager.GetString("NewPostBody", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to New post on .
        /// </summary>
        public static string NewPostTitle {
            get {
                return ResourceManager.GetString("NewPostTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} created the topic &apos;{1}&apos; on {2}.
        /// </summary>
        public static string NewTopicBody {
            get {
                return ResourceManager.GetString("NewTopicBody", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to New topic on .
        /// </summary>
        public static string NewTopicTitle {
            get {
                return ResourceManager.GetString("NewTopicTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} uploaded the video &apos;{1}&apos; on {2}.
        /// </summary>
        public static string NewVideoBody {
            get {
                return ResourceManager.GetString("NewVideoBody", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to New video on .
        /// </summary>
        public static string NewVideoTitle {
            get {
                return ResourceManager.GetString("NewVideoTitle", resourceCulture);
            }
        }
    }
}
