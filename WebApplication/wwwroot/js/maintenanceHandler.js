function MaintenanceHandler() {

    const uri = 'api/Maintenance';

    function getAllDatabases() {
        return $.getJSON(uri + "");
    }

    function createNewDatabase(databaseName) {
        return $.getJSON(uri + "/createNewDatabase", { databaseName: databaseName });
    }

    function saveDatabase(databaseDto) {
        return $.post(uri, { databaseDto: databaseDto });
    }

    return {
        getAllDatabases: getAllDatabases,
        createNewDatabase: createNewDatabase,
        saveDatabase: saveDatabase,
    }
}