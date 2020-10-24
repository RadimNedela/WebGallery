function MaintenanceHandler() {

    const uri = 'api/Maintenance';

    function getAllDatabases() {
        return $.getJSON(uri + "");
    }

    function createNewDatabase(databaseName) {
        return $.getJSON(uri + "/createNewDatabase", { databaseName: databaseName });
    }

    return {
        getAllDatabases: getAllDatabases,
        createNewDatabase: createNewDatabase,
    }
}