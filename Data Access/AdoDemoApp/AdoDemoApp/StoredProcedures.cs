namespace AdoDemoApp;
internal static class StoredProcedures
{
    internal static class Students
    {
        internal const string Create = "sp_CreateStudent";
        internal const string Get = "sp_GetStudent";
        internal const string Update = "sp_UpdateStudent";
        internal const string GetAll = "sp_GetStudents";
        internal const string Delete = "sp_DeleteStudent";
        internal const string FindByName = "sp_FindStudentByName";
    }
}
