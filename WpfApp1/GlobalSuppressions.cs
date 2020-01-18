// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[ assembly:
    SuppressMessage(
                       "Usage", "VSTHRD001:Avoid legacy thread switching APIs",
                       Justification = "<Pending>", Scope = "member",
                       Target =
                           "~M:WpfApp1.App.Application_Startup(System.Object,System.Windows.StartupEventArgs)"
                   ) ]