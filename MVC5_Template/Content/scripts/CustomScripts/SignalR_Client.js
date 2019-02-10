$(document).ready(function () { 

  var myHub = $.connection.MVC5_TemplateHub;
  $.connection.hub.start();

  myHub.client.showNotificationBar = function () {
      AddCookieForNotificationBar();
  };
});


