﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Agenda.Domain.Errors {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class ResourceMessageError {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ResourceMessageError() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Agenda.Domain.Errors.ResourceMessageError", typeof(ResourceMessageError).Assembly);
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
        ///   Looks up a localized string similar to There is already a client scheduled for this time..
        /// </summary>
        public static string ALREADY_CLIENT_SCHEDULE {
            get {
                return ResourceManager.GetString("ALREADY_CLIENT_SCHEDULE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to .
        /// </summary>
        public static string APPOINTMENT_HOUR_CANNOT_DAY_OFF {
            get {
                return ResourceManager.GetString("APPOINTMENT_HOUR_CANNOT_DAY_OFF", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The appointment cannot be canceled less than 2 hours in advance..
        /// </summary>
        public static string LESS_THAN_TWO_HOURS_BEFORE {
            get {
                return ResourceManager.GetString("LESS_THAN_TWO_HOURS_BEFORE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to There is not a client scheduled for this time..
        /// </summary>
        public static string NO_CLIENT_SCHEDULE {
            get {
                return ResourceManager.GetString("NO_CLIENT_SCHEDULE", resourceCulture);
            }
        }
    }
}
