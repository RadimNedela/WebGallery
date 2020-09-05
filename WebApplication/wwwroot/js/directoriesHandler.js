function DirectoriesHandler() {

    const uri = 'api/Directories';

    function getDirectoryContent(directoryName) {
        return $.getJSON(uri + "/getDirectoryContent", { directoryName: directoryName });
    }

    return {
        getDirectoryContent: getDirectoryContent
    }
}