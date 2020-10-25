"use strict";

function Maintenance() {

    var MH = MaintenanceHandler();
    var allDatabases;

    function allDatabasesArrived(data) {
        allDatabases = data;
        var $table = $("#DatabasesTable");

        var $tableBody = $("#DatabasesTableBody");
        $tableBody.empty();

        for (let i = 0; i < data.length; i++) {
            addDatabaseDtoInfoRow($table, data[i]);
        }
        addNewDatabaseInfoRow($table);
    }

    function addNewDatabaseInfoRow($table) {
        var $tr = $table.find('tbody:last').append('<tr>');
        var $input = $('<input type="text" id="NewDatabaseNameInput" />');
        var $td = $('<td> </td>').html($input);
        $tr.append($td);

        var $button = $('<input type="button" value="Create New Database"/>');
        $button.click(createNewDatabase);
        $td = $('<td> </td>').html($button);
        $tr.append($td);
    }

    function inputFieldValueChanged() {
        var dto = $(this).data("dto");
        var propertyName = $(this).data("propertyName");
        var index = $(this).data("index");
        var newValue = $(this).val();

        if (typeof index !== "undefined") {
            if (index == dto[propertyName].length)
                dto[propertyName].push(newValue);
            else
                dto[propertyName][index] = newValue;
        }
        else dto[propertyName] = newValue;
    }

    function createInputField(dto, propertyName, index) {
        var propertyValue = dto[propertyName];
        if (typeof index !== "undefined") {
            if (index < propertyValue.length)
                propertyValue = propertyValue[index];
            else
                propertyValue = "";
        }

        var $input = $('<input type="text" value="' + propertyValue + '" />');
        $input.data("dto", dto);
        $input.data("propertyName", propertyName);
        if (typeof index !== "undefined") $input.data("index", index);
        $input.on('input', inputFieldValueChanged);
        return $input;
    }

    function addDatabaseDtoInfoRow($table, databaseDto) {
        var $tr = $table.find('tbody:last').append('<tr>');
        var $nameInput = createInputField(databaseDto, "name");

        var $td = $('<td> </td>').html($nameInput);
        $tr.append($td);
        $tr.append('<td>' + databaseDto.hash + '</td>');
        $tr.append('<td>' + " " + '</td>');
        $tr.append('<td>' + " " + '</td>');
        $tr.append('<td>' + " " + '</td>');

        var $action = $('<input type="button" value="Save Database"/>');
        $action.click({ databaseDto: databaseDto }, saveDatabase);
        $td = $('<td> </td>').html($action);
        $tr.append($td);
        

        for (let i = 0; i < databaseDto.racks.length; i++) {
            addRackInfoRow($table, databaseDto, databaseDto.racks[i]);
        }
    }

    function addRackInfoRow($table, databaseDto, rackDto) {
        var $tr = $table.find('tbody:last').append('<tr>');
        $tr.append('<td>' + " " + '</td>');
        $tr.append('<td>' + " " + '</td>');
        var $td = $('<td> </td>').html(createInputField(rackDto, "name"));
        $tr.append($td);
        $tr.append('<td>' + rackDto.hash + '</td>');
        $tr.append('<td>' + " " + '</td>');

        var $action = $('<input type="button" value="Add New Rack"/>');
        $action.click({ databaseDto: databaseDto }, addNewRack);
        $td = $('<td> </td>').html($action);
        $tr.append($td);

        for (let i = 0; i < rackDto.mountPoints.length; i++) {
            addMountPointRow($table, databaseDto, rackDto, i);
        }
    }

    function addMountPointRow($table, databaseDto, rackDto, index) {
        var $tr = $table.find('tbody:last').append('<tr>');
        $tr.append('<td>' + " " + '</td>');
        $tr.append('<td>' + " " + '</td>');
        $tr.append('<td>' + " " + '</td>');
        $tr.append('<td>' + " " + '</td>');
        var $td = $('<td> </td>').html(createInputField(rackDto, "mountPoints", index));
        $tr.append($td);

        var $action = $('<input type="button" value="Add New Mount Point"/>');
        $action.click({ databaseDto: databaseDto, rackDto: rackDto }, addNewMountPoint);
        $td = $('<td> </td>').html($action);
        $tr.append($td);
    }

    function createNewDatabase() {
        var newName = $('#NewDatabaseNameInput').val();
        if (newName) {
            MH.createNewDatabase(newName)
                .then(allDatabasesArrived);
        }
    }

    function addNewRack(e) {
        var databaseDto = e.data.databaseDto;
        MH.addNewRack(databaseDto)
            .then(allDatabasesArrived);
    }

    function addNewMountPoint(e) {
        var databaseDto = e.data.databaseDto;
        var rackDto = e.data.rackDto;
        MH.addNewMountPoint(databaseDto, rackDto)
            .then(allDatabasesArrived);
    }

    function saveDatabase(e) {
        var databaseDto = e.data.databaseDto;
        MH.saveDatabase(databaseDto)
            .then(allDatabasesArrived);
    }

    $(function () {
        MH.getAllDatabases()
            .then(allDatabasesArrived);
    });
}

Maintenance();
