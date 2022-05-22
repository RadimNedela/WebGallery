function DirectoriesHandler() {

    const uri = 'api/Directories';

    function getDirectoryContent(directoryName) {
        return $.getJSON(uri + "/getDirectoryContent", { directoryName: directoryName });
    }

    function getImage(hash) {
        return $.getJSON(uri + "/getImage/" + hash);
    }

    return {
        getDirectoryContent: getDirectoryContent,
        getImage: getImage,
    }
}