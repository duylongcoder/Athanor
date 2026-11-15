// Browser hooks for the Game layer: vibration (Haptics.cs) and tab visibility (AudioMute.cs).
mergeInto(LibraryManager.library, {
  // 1 on a touch device whose browser has navigator.vibrate (Android Chrome); 0 on desktop and iOS
  AthanorCanVibrate: function () {
    var touch = (navigator.maxTouchPoints || 0) > 0 && window.matchMedia && window.matchMedia('(pointer: coarse)').matches;
    return (typeof navigator.vibrate === 'function' && touch) ? 1 : 0;
  },
  AthanorVibrate: function (ms) {
    if (typeof navigator.vibrate === 'function') navigator.vibrate(ms);
  },
  // calls cb(1) when the tab is hidden and cb(0) when it is shown again
  AthanorWatchVisibility: function (cb) {
    document.addEventListener('visibilitychange', function () {
      {{{ makeDynCall('vi', 'cb') }}}(document.hidden ? 1 : 0);
    });
  }
});
