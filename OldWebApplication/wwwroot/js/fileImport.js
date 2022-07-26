"use strict";

function FileImport() {

    var FIH = FileImportHandler();
    var rackInfo;
    var currentDirectoryInfo;

    function rackInfoArrived(data) {
        rackInfo = data;
        $('#ActiveDatabaseNameDiv').html(rackInfo.activeDatabaseName);
        $('#ActiveDatabaseHashDiv').html(rackInfo.activeDatabaseHash);
        $('#ActiveRackNameDiv').html(rackInfo.activeRackName);
        $('#ActiveRackHashDiv').html(rackInfo.activeRackHash);

        directoryInfoArrived(rackInfo.directoryInfo);
    }

    function directoryInfoArrived(directoryInfo) {
        currentDirectoryInfo = directoryInfo;
        fillDirectoriesTable(directoryInfo);
        fillFilesTable(directoryInfo);
    }

    function subDirectoryClicked(e) {
        var subDirectory = e.data.subDirectory;
        FIH.getDirectoryInfo(rackInfo.activeRackHash, subDirectory)
            .then(directoryInfoArrived);
    }

    function fillDirectoriesTable(directoryInfo) {
        var $dirTableBody = $('#DirectoriesListBody');
        $dirTableBody.empty();
        for (let i = 0; i < directoryInfo.subDirectories.length; i++) {
            addDirectory($dirTableBody, directoryInfo.subDirectories[i]);
        }
    }

    function addDirectory($dirTableBody, subDirectory) {
        var $tr = $dirTableBody.append('<tr>');
        var label = subDirectory;
        if (label.endsWith(".."))
            label = "..";
        var $td = $('<td> </td>').html(label);
        $td.click({ subDirectory: subDirectory }, subDirectoryClicked);
        $tr.append($td);
    }

    function fillFilesTable(directoryInfo) {
        var $filesTableBody = $('#FilesListBody');
        $filesTableBody.empty();
        for (let i = 0; i < directoryInfo.files.length; i++) {
            addFile($filesTableBody, directoryInfo.files[i]);
        }
    }

    function addFile($filesTableBody, file) {
        var $tr = $filesTableBody.append('<tr>');
        var $td = $('<td> </td>').html(file);
        $tr.append($td);
    }

    function parseFiles() {
        FIH.parseDirectoryContent(rackInfo.activeRackHash, currentDirectoryInfo.currentDirectory)
            .then(parseDone);
    }

    function getThreadInfo() {
        FIH.getThreadInfo(rackInfo.activeRackHash, currentDirectoryInfo.currentDirectory)
            .then(directoryContentThreadInfoDtoArrived);
    }

    function parseDone(data) {
        directoryContentThreadInfoDtoArrived(data);
    }

    function directoryContentThreadInfoDtoArrived(data) {
        if (data) {
            $('#ThreadTableFiles').html(data.files);
            $('#ThreadTableBytes').html(data.bytes);
            $('#ThreadTableFilesDone').html(data.filesDone);
            $('#ThreadTableBytesDone').html(data.bytesDone);
        }
    }

    $(function () {
        const urlParams = new URLSearchParams(window.location.search);
        var rackHash = urlParams.get("rackHash");
        $('#ParseFilesButton').click(parseFiles);
        $('#GetParseInfoButton').click(getThreadInfo);

        FIH.getRackInfo(rackHash)
            .then(rackInfoArrived);
    });
}

FileImport();
