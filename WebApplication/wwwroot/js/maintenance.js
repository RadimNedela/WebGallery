function Maintenance() {

    var MH = MaintenanceHandler();
    var allDatabases;

    function allDatabasesArrived(data) {
        allDatabases = data;
        var $table = $("#DatabasesTable");

        for (let i = 0; i < data.length; i++) {
            addDatabaseDtoInfoRow($table, data[i]);
        }
    }

    function showActiveDatabase(event) {
        var hash = event.data.hash;
        var db;
        for (let i = 0; i < allDatabases.length; i++) {
            if (allDatabases[i].hash === hash) {
                db = allDatabases[i];
                break;
            }
        }
        if (db) {
            $('#ActiveDatabaseNameInput').val(db.name);
            $('#ActiveDatabaseHashInput').val(db.hash);
        }
    }

    function addDatabaseDtoInfoRow($table, dto) {
        var $tr = $table.find('tbody:last').append('<tr>');
        var $td = $('<td>' + dto.name + '</td>');
        $td.click({ hash: dto.hash }, showActiveDatabase);
        $tr.append($td);
        $tr.append('<td>' + dto.hash + '</td>');

        for (let i = 0; i < dto.racks.length; i++) {
            addRackInfoRow($table, dto.racks[i]);
        }
    }

    function addRackInfoRow($table, rackDto) {
        var $tr = $table.find('tbody:last').append('<tr>');
        $tr.append('<td>' + " " + '</td>');
        $tr.append('<td>' + " " + '</td>');
        $tr.append('<td>' + rackDto.name + '</td>');
        $tr.append('<td>' + rackDto.hash + '</td>');

        for (let i = 0; i < rackDto.mountPoints.length; i++) {
            addMountPointRow($table, rackDto.mountPoints[i]);
        }
    }

    function addMountPointRow($table, mountPoint) {
        var $tr = $table.find('tbody:last').append('<tr>');
        $tr.append('<td>' + " " + '</td>');
        $tr.append('<td>' + " " + '</td>');
        $tr.append('<td>' + " " + '</td>');
        $tr.append('<td>' + " " + '</td>');
        $tr.append('<td>' + '"' + mountPoint + '"' + '</td>');
    }

    $(function () {
        MH.getAllDatabases()
            .then(allDatabasesArrived);
        //$("#MainImageMaxWidthInput").on('input', mainImageMaxWidthChanged);
        //$("#ParseDirectoryButton").click(parseDirectoryContent);
    });
}

Maintenance();
