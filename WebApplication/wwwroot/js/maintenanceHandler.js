function MaintenanceHandler() {

    const uri = 'api/Maintenance';

    function getAllDatabases() {
        return $.getJSON(uri + "");
    }

    return {
        getAllDatabases: getAllDatabases,
    }
}