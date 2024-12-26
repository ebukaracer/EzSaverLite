mergeInto(LibraryManager.library, {
    setLocalStorage: function(key, value) {
        try {
            // Use UTF8ToString to convert C strings to JavaScript strings
            localStorage.setItem(UTF8ToString(key), UTF8ToString(value));
        } catch (error) {
            console.error("Error saving to local storage: " + error);
        }
    },

    getLocalStorage: function(key, defaultValue) {
        var result = UTF8ToString(defaultValue);
        
        try {
            result = localStorage.getItem(UTF8ToString(key));
            
            // Check if the result is undefined or null and set it to "defaultValue" if necessary
            if (result === undefined || result === null) {
                result = UTF8ToString(defaultValue);
            }
        } catch (error) {
            console.log("Error retrieving from local storage: " + error);
        }
         // Convert the JavaScript string to a C string and return the buffer
        var bufferSize = lengthBytesUTF8(result) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(result, buffer, bufferSize);
        return buffer;
    },

    removeLocalStorage: function(key) {
        try {
            localStorage.removeItem(UTF8ToString(key));
        } catch (error) {
            console.error("Error removing from local storage: " + error);
        }
    },

    clearLocalStorage: function() {
        try {
            localStorage.clear();
        } catch (error) {
            console.error("Error clearing local storage: " + error);
        }
    }
});