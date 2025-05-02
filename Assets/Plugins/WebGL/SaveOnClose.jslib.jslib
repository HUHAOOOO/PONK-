mergeInto(LibraryManager.library, {
  SetupBeforeUnload: function () {
    window.addEventListener("beforeunload", function (e) {
      SendMessage("SaveLoadManager", "OnTabClose");
    });
  }
});
