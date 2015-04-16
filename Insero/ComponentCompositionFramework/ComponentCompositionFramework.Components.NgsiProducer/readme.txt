This component requires the following configuration in your app.config or web.config file of your startup project:

<configuration>
   <appSettings>
      <add key="ngsi:PublishSubscriberEndpoint" value="http://130.206.81.223:1026" />
   </appSettings>
</configuration>

Where the value is the http endpoint of the publish subscribe context broker GE.