function MaintenanceHandler() {

    const uri = 'api/Maintenance';

    function getAllDatabases() {
        return $.getJSON(uri + "");
    }

    function createNewDatabase(databaseName) {
        return $.getJSON(uri + "/createNewDatabase", { databaseName: databaseName });
    }

    function addNewRack(databaseDto) {
        return $.post(uri + "/addNewRack", { databaseDto: databaseDto });
    }

    function addNewMountPoint(database, rack) {
        var dto = { databaseHash: database.hash, rackHash: rack.hash };
        return $.post(uri + "/addNewMountPoint", { dto: dto });
    }

    function saveDatabase(databaseDto) {
        return $.post(uri, { databaseDto: databaseDto });
    }

    return {
        getAllDatabases: getAllDatabases,
        createNewDatabase: createNewDatabase,
        addNewRack: addNewRack,
        addNewMountPoint: addNewMountPoint,
        saveDatabase: saveDatabase,
    }
}