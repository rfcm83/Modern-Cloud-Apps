![Microsoft Cloud Workshops](../Media/ms-cloud-workshop.png "Microsoft Cloud Workshops")

# Modern Cloud Apps

Before the hands-on lab setup guide

March 2020

## Table Of Contents
- [Modern cloud apps hands-on lab step-by-step](#modern-cloud-apps-hands-on-lab-step-by-step)
  - [Abstract and learning objectives](#abstract-and-learning-objectives)
  - [Overview](#overview)
  - [Solution architecture](#solution-architecture)
  - [Requirements](#requirements)
  - [Help references](#help-references)
  - [Exercise 5: Automating backend processes with Azure Functions and Logic Apps](#exercise-5-automating-backend-processes-with-azure-functions-and-logic-apps)
    - [Task 1: Create an Azure Function to Generate PDF Receipts](#task-1-create-an-azure-function-to-generate-pdf-receipts)
    - [Task 2: Create an Azure Logic App to Process Orders](#task-2-create-an-azure-logic-app-to-process-orders)
    - [Task 3: Use Twilio to send SMS Order Notifications](#task-3-use-twilio-to-send-sms-order-notifications)
      - [Subtask 1: Configure your Twilio trial account](#subtask-1-configure-your-twilio-trial-account)
      - [Subtask 2: Create a new logic app](#subtask-2-create-a-new-logic-app)

# Modern cloud apps hands-on lab step-by-step

## Abstract and learning objectives

In this hands-on lab, you will be challenged to implement an end-to-end scenario using a supplied sample that is based on Azure App Services, Microsoft Azure Functions, Azure SQL Database, Azure Logic Apps, and related services. The scenario will include implementing compute, storage, workflows, and monitoring, using various components of Microsoft Azure.

Please note that as opposed to the whiteboard design session, the lab is not focused on maintaining PCI compliance and using more advanced security features such as App Service Environment, Network Security Groups, and Application Gateway. The hands-on lab can be implemented on your own, but it is highly recommended to pair up with other members working on the lab to model a real-world experience and to allow each member to share their expertise for the overall solution.

By the end of this hands-on lab, you will have learned how to use several key services within Azure to improve overall functionality of the original solution, and to increase the security and scalability of the new and improved design.

## Overview

The Cloud Workshop: Modern Cloud Apps lab is a hands-on exercise that will challenge you to implement an end-to-end scenario using a supplied sample that is based on Microsoft Azure App Services and related services. The scenario will include implementing compute, storage, security, and scale using various components of Microsoft Azure. The lab can be implemented on your own, but it is highly recommended to pair up with additional team members to more closely model a real-world experience, and to allow members to share their expertise for the overall solution.

## Solution architecture

![A diagram that depicts the various Azure PaaS services for the solution. Azure AD Org is used for authentication to the call center app. Azure AD B2C for authentication is used for authentication to the client app. SQL Database for the backend customer data. Azure App Services for the web and API apps. Order processing includes using Functions, Logic Apps, Queues and Storage. Azure App Insights is used for telemetry capture.](../Media/Screenshots/image2.png "Solution Architecture Diagram")

## Requirements

1. Microsoft Azure subscription
2. Local machine or a virtual machine configured with Visual Studio 2019 Community Edition
3. Twilio account and/or personal cell phone to setup a trial Twilio account

## Help references

| Description | Links |
|:---------|:-------------|
| SQL firewall | <https://azure.microsoft.com/en-us/documentation/articles/sql-database-configure-firewall-settings/> |
| Deploying a Web App | <https://azure.microsoft.com/en-us/documentation/articles/web-sites-deploy/> |
| Deploying an API app | <https://azure.microsoft.com/en-us/documentation/articles/app-service-dotnet-deploy-api-app/> |
| Accessing an API app from a JavaScript client | <https://azure.microsoft.com/en-us/documentation/articles/app-service-api-javascript-client/> |
| SQL Database Geo-Replication overview | <https://azure.microsoft.com/en-us/documentation/articles/sql-database-geo-replication-overview/> |
| What is Azure AD? | <https://azure.microsoft.com/en-us/documentation/articles/active-directory-whatis/> |
| Azure Web Apps authentication | <http://azure.microsoft.com/blog/2014/11/13/azure-websites-authentication-authorization/> |
| View your access and usage reports | <https://msdn.microsoft.com/en-us/library/azure/dn283934.aspx> |
| Custom branding an Azure AD Tenant | <https://msdn.microsoft.com/en-us/library/azure/Dn532270.aspx> |
| Service Principal Authentication | <https://docs.microsoft.com/en-us/azure/app-service-api/app-service-api-dotnet-service-principal-auth> |
| Consumer Site B2C | <https://docs.microsoft.com/en-us/azure/active-directory-b2c/active-directory-b2c-devquickstarts-web-dotnet> |
| Getting Started with Active Directory B2C | <https://azure.microsoft.com/en-us/trial/get-started-active-directory-b2c/> |
| How to Delete an Azure Active Directory | <https://blog.nicholasrogoff.com/2017/01/20/how-to-delete-an-azure-active-directory-add-tenant/> |
| Run performance tests on your app | <http://blogs.msdn.com/b/visualstudioalm/archive/2015/09/15/announcing-public-preview-for-performance-load-testing-of-azure-webapp.aspx> |
| Application Insights Custom Events | <https://azure.microsoft.com/en-us/documentation/articles/app-insights-api-custom-events-metrics/> |
| Enabling Application Insights | <https://azure.microsoft.com/en-us/documentation/articles/app-insights-start-monitoring-app-health-usage/> |
| Detect failures | <https://azure.microsoft.com/en-us/documentation/articles/app-insights-asp-net-exceptions/> |
| Monitor performance problems | <https://azure.microsoft.com/en-us/documentation/articles/app-insights-web-monitor-performance/> |
| Creating a Logic App | <https://azure.microsoft.com/en-us/documentation/articles/app-service-logic-create-a-logic-app/> |
| Logic app connectors | <https://azure.microsoft.com/en-us/documentation/articles/app-service-logic-connectors-list/> |
| Logic Apps Docs | <https://docs.microsoft.com/en-us/azure/logic-apps/logic-apps-what-are-logic-apps> |
| Azure Functions -- create first function | <https://docs.microsoft.com/en-us/azure/azure-functions/functions-create-first-azure-function> |
| Azure Functions docs | <https://docs.microsoft.com/en-us/azure/logic-apps/logic-apps-azure-functions> |

## Exercise 5: Automating backend processes with Azure Functions and Logic Apps

Contoso wants to automate the process of generating receipts in PDF format and alerting users when their orders have been processed using Azure Logic App and Functions. To run custom snippets of C\# or node.js in logic apps, you can create custom functions through Azure Functions. [Azure Functions](https://docs.microsoft.com/en-us/azure/azure-functions/functions-overview) offers server-free computing in Microsoft Azure and are useful for performing these tasks:

- Advanced formatting or compute of fields in logic apps

- Perform calculations in a workflow

- Extend the logic app functionality with functions that are supported in C\# or node.js

### Task 1: Create an Azure Function to Generate PDF Receipts

1. Select the **+Create a resource** button found on the upper left-hand corner of the Azure portal and then select **Compute \> Function App**. Select **Create** button at the bottom.

    ![On the left side of the Portal, the Create a resource button is selected. In the middle, under New, Compute is selected. On the right, under Compute, Function App is selected.](../Media/Screenshots/image221.png "Azure Portal")

2. Provision and deploy the new function app, with the following settings:

    - [**Resource Group**](https://docs.microsoft.com/en-us/azure/azure-resource-manager/resource-group-overview): Use the existing resource group, **contososports**.

    - **Runtime Stack**: .NET Core.

    - **Region**: Choose the same region used for the e-commerce web apps in this lab.

3. Select **Next: Hosting >**.

4. On the **Hosting** tab, select the following values, then select **Review + create**:

    - **Operating System**: Windows.

    - **Plan type**: App service plan.

    - **Windows Plan**: Choose the App Service Plan used for the e-commerce web app.

5. Navigate to the Storage Account in the **contososports** resource group, go to **Access Keys** and copy the **Connection String** for the Storage Account. Paste your storage account connection string into Notepad to save for later.

    ![Display storage account list.  Pointing to Access keys.](../Media/Screenshots/2019-04-15-15-07-15.png "Storage Account Access keys")

6. Navigate to the **Function App** that was just created, and select **Configuration**.

    ![Display Contoso Function App, with the Configuration link highlighted.](../Media/Screenshots/2019-04-15-15-15-22.png "Contoso Function App Application Settings")

7. Add a new Application Setting with the following values, then select **Save**:

    - **Name**: `contososportsstorage`.
    - **Value**: Enter the Connection String for your storage account.

    ![Updated Function App Application settings. Showing final values.](../Media/Screenshots/2019-04-15-16-18-36.png "Updated Function App Application settings.")

8. To publish the Function App, open the Visual Studio solution, Right-click on the **ContosoFunctionApp** project, then select **Publish**.

    ![Visual Studio Solution Explorer is open. Menu is displayed for Contoso Function App. Selecting function app publish.](../Media/Screenshots/2019-04-15-15-31-03.png "Selecting function app publish")

9. On the **Pick a publish target** dialog, choose **Select existing**, then select **Create Profile**.

10. Select the **Function App**, then select **OK**.

    ![Azure function app tree displayed. The Contoso Function App is selected.](../Media/Screenshots/2019-04-15-15-34-54.png "Azure function app tree displayed")

11. Select **Publish**.

    The publish should only take minute or so. You can check the **Output** window for any errors that may occur.

    ![The build Output window is displayed. Publish succeeded message is shown.](../Media/Screenshots/2019-04-15-15-33-20.png "Output window.")

12. To test your newly published Function App, start by navigating back to your Contoso Function App in the Azure Portal. Select the newly created **ContosoMakePDF** function listed in the functions.

13. select the **Test** link located on the right-hand blade.

    ![Function apps are listed on the left hand side. ContosoMakePDF is selected.  There is an arrow pointing to the Test link on the right pane.](../Media/Screenshots/2019-04-15-15-40-27.png "Function Test link")

14. Select **POST** for the HTTP method.

15. Open the **sample.dat** file found in your lab files Contoso.CreatePDFReport directory.  Copy the contents into the **Request body** text box.

    ![A small screenshot of Windows Explorer is shown emphasizing the file path to the sample.dat file.](../Media/Screenshots/2019-04-15-15-47-39.png "Sample.dat File")

16. Select the **Run** button located at the bottom of the blade.

    ![The screenshot displays the Test blade with sample.dat contents. The Request body field shows the Order JSON. There is an arrow pointing to Run button.](../Media/Screenshots/2019-04-15-15-52-59.png "Display Test blade with sample.dat contents")

    > **Note**: There is also a **Run** button located at the top of the Azure Function blade. Selecting either of these buttons will run the function just the same.

    After a few seconds, you should see logs like in the below image. You should see return status code of 200.  The **Output** text box should show recent Contoso purchase data. You should see a message stating the file has been created and stored in the blob storage.

    ![There is a screenshot displaying the Function App test result log.  A status code of 200 OK is displayed on the right side pane.](../Media/Screenshots/2019-04-15-15-58-54.png "Function App test result log.")

16. Check your receipt PDF in the storage account blob.

    - Navigate to the ContosoSports storage account.
    - Select the **Blobs** link.

    ![The Settings options are displayed. There is an arrow pointing to the Blobs link.](../Media/Screenshots/2019-04-15-16-06-17.png "Blobs link")

17. Choose the newly created **receipts** blob container.

    ![The storage account blobs are listed. Receipts blob container is highlighted.](../Media/Screenshots/2019-04-15-16-08-35.png "Click the Blobs link")

18. Open **ContosoSportsLeague-Store-Receipt-XX.pdf** link.

    ![There is a screenshot displaying a list of the newly created PDF receipts. An arrow pointing to the Download link is located on the right side of the screen.](../Media/Screenshots/2019-04-15-16-11-24.png "PDF Receipts")

    - Open the `...` link and choose download menu item.

    ![A sample Contoso Sports League PDF receipt is displayed.](../Media/Screenshots/2019-04-15-16-15-06.png "Sample PDF receipt")

### Task 2: Create an Azure Logic App to Process Orders

Without writing any code, you can automate business processes more easily and quickly when you create and run workflows with Azure Logic Apps. Logic Apps provide a way to simplify and implement scalable integrations and workflows in the cloud. It provides a visual designer to model and automate your process as a series of steps known as a workflow. There are [many connectors](https://docs.microsoft.com/en-us/azure/connectors/apis-list) across the cloud and on-premises to quickly integrate across services and protocols.

The advantages of using Logic Apps include the following:

- Saving time by designing complex processes using easy to understand design tools

- Implementing patterns and workflows seamlessly, that would otherwise be difficult to implement in code

- Getting started quickly from templates

- Customizing your logic app with your own custom APIs, code, and actions

- Connect and synchronize disparate systems across on-premises and the cloud

- Build off BizTalk server, API Management, Azure Functions, and Azure Service Bus with first-class integration support

1. Next, we will create a Logic App that will trigger when an item is added to the **receiptgenerator** queue. In the Azure Management Portal, select the **+Create a resource** button, search for **Logic App**, select the returned Logic App result, and select **Create**.

    ![In the Azure Portal, under Marketplace, Everything is selected. Under Everything, Logic App is in the search field. Under Name, Logic App is circled.](../Media/Screenshots/image236.png "Azure Portal")

2. Fill out the name as **ContosoLogicApplication** along with your subscription, and use the existing resource group **contososports**. Choose the **same region** as your Web App and storage account. Select **Create**.

    ![In the Create logic app blade, ContosoLogicApplication is in the Name field. Under Resource group, the Use existing radio button is selected, and contososports is the name.](../Media/Screenshots/image237.png "Create logic app blade")

3. Open the logic app after it is deployed by choosing **All Services**, searching for and selecting **Logic App** and selecting the Logic App you just created.

    ![In the Azure Portal, logic is in the search field, and under that, Logic apps is selected.](../Media/Screenshots/image238.png "Azure Portal")

4. select the **Logic App Designer** link.

    ![In the Logic app blade, under Development tools, Logic App Designer is selected.](../Media/Screenshots/image239.png "Logic app blade")

5. In the Logic Apps Designer, under **Templates**, select **Blank Logic App**.

    ![In the Logic Apps Designer, the Blank Logic App tile is selected.](../Media/Screenshots/2019-03-29-12-56-10.png "Logic Apps Designer")

6. Search for **Azure Queues**.

    ![In the Services section, the Azure Queues tile is selected.](../Media/Screenshots/image241.png "Services section")

7. Select **Azure Queues -- When there are messages in a queue**.

    ![In the Search all triggers section, Azure Queues - When there are messages in a queue is selected.](../Media/Screenshots/image242.png "Search all triggers section")

8. Specify **ContosoStorage** as the connection name, select the Contoso storage account from the list, and select **Create**.

    ![In When there are messages in a queue, the Connection Name is ContosoStorage, and under Storage Account, contosostorage123321 is selected.](../Media/Screenshots/image243.png "When there are messages in a queue ")

9. Select the **receiptgenerator** queue from the drop-down, select **New Step**, and **Add an Action**.

    ![Under When there are messages in a queue, the Queue name is set to receiptgenerator. At the bottom, the New Step and Add an action buttons are selected.](../Media/Screenshots/image244.png "Queue name")

10. Select **Azure Functions**.

    ![In the Choose an action section, under Services ,the Azure Functions tile is selected.](../Media/Screenshots/image245.png "Choose an action")

11. Select the **Azure Function App** you just created.

    ![Under Azure Functions, on the Actions tab, a single Action, the Azure function contosofunction2101, is listed.](../Media/Screenshots/image246.png "Azure Functions")

12. Select the Azure function **ContosoMakePDF**.

    ![Under Azure Functions, on the Actions tab, a single Action, the Azure function contosofunction2101, is listed.](../Media/Screenshots/image247.png "Azure Functions")

13. Type this in the Request Body:

    ```json
    {"Order": pick MessageText from list on right }
    ```

    Make sure the syntax is json format. Sometimes the ":" will go to the right side of MessageText by mistake. Keep it on the left. It should look like this:

    ![Under ContosoMakePDF, the previous JSON code is typed in the Request Body, and to the right of this, in Insert parameters from previous steps, Message text is selected.](../Media/Screenshots/image248.png "ContosoMakePDF")

14. Select **Save** to save the Logic App.

15. There is one modification we need to make in the code. select the **CodeView** button.

    ![In the Logic App, the CodeView button is selected from the top menu.](../Media/Screenshots/image250.png "Logic App")

16. Find the line of code in the body for the Order item that reads the MessageText value from the queue, and add the base64 function around it to ensure it encoded before passing it off to the Azure function. It should look like the following:

    ```json
    "Order": "@{base64(triggerBody()?['MessageText'])}"
    ```

    ![In the Order item code, the following line of code is circled: \"Order\": \"@{base64(triggerBody()?\[\'MessageText\'\])}\"](../Media/Screenshots/image251.png "Order item code")

17. Select **Save** again.

18. Run the logic app. It should process the orders you have submitted previously to test PDF generation. Using Azure Storage Explorer or Visual Studio Cloud Explorer you can navigate to the storage account and open the receipts container to see the created PDFs.

    ![In Azure Storage Explorer, on the left, the following tree view is expanded: Storage Accounts\\contososportsstorage01r\\Blob Containers. Under Blob Containers, receipts is selected. On the right, the ContosoSportsLeague-Store-Receipt-72.pdf is selected.](../Media/Screenshots/image252.png "Azure Storage Explorer")

18. Double-click it to see the Purchase receipt.

19. Now, select the **Designer** button in the Logic Apps Designer screen. add two more steps to the flow for updating the database and removing the message from the queue after it has been processed. Switch back to the designer, select **+ New step**.

    ![In Designer, the New Step link is circled. Under New step, the Add an action tile is circled.](../Media/Screenshots/image254.png "Designer")

20. Select **SQL Server**.

    ![In the Services section, under Services, SQL Server is selected.](../Media/Screenshots/image255.png "Services section")

21. Select **SQL Server - Update row**.

    ![In the SQL Server section, on the Actions tab, SQL Server - Update row is selected.](../Media/Screenshots/image256.png "SQL Server section")

22. Name the connection `ContosoSportsDB`, and select the primary ContosoSportsDB database for your solution. Under the user name and password used to create it, select **Create**.

    ![The Update row section displays the previously defined settings.](../Media/Screenshots/image257.png "Update row")

23. From the drop-down select the name of the table, **Orders**.

    ![In the Update row section, under Table name, Orders is selected.](../Media/Screenshots/image258.png "Update row section")

24. Press **Save** and ignore the error. Select the **Code View** button.

25. Replace these lines:

    ![Screenshot of code to be replaced.](../Media/Screenshots/image259.png "Code view")

    With these:

    ```json
    "OrderDate": "@{body('ContosoMakePDF')['OrderDate']}",
    "FirstName": "@{body('ContosoMakePDF')['FirstName']}",
    "LastName": "@{body('ContosoMakePDF')['LastName']}",
    "Address": "@{body('ContosoMakePDF')['Address']}",
    "City": "@{body('ContosoMakePDF')['City']}",
    "State": "@{body('ContosoMakePDF')['State']}",
    "PostalCode": "@{body('ContosoMakePDF')['PostalCode']}",
    "Country": "@{body('ContosoMakePDF')['Country']}",
    "Phone": "@{body('ContosoMakePDF')['Phone']}",
    "SMSOptIn": "@{body('ContosoMakePDF')['SMSOptIn']}",
    "SMSStatus": "@{body('ContosoMakePDF')['SMSStatus']}",
    "Email": "@{body('ContosoMakePDF')['Email']}",
    "ReceiptUrl": "@{body('ContosoMakePDF')['ReceiptUrl']}",
    "Total": "@{body('ContosoMakePDF')['Total']}",
    "PaymentTransactionId": "@{body('ContosoMakePDF')['PaymentTransactionId']}",
    "HasBeenShipped": "@{body('ContosoMakePDF')['HasBeenShipped']}"
    ```

26. And modify the path variable to include the index key or OrderId to be as follows:

    ```json
    "path": "/datasets/default/tables/@{encodeURIComponent(encodeURIComponent('[dbo].[Orders]'))}/items/@{encodeURIComponent(encodeURIComponent(body('ContosoMakePDF')['OrderId']))}"
    ```

    The code should now look as follows for the update\_row method:

    ![Screenshot of replacement code.](../Media/Screenshots/image260.png "Code")

27. **Save** and return to the designer.

28. Your updated designer view should look like this:

    ![The Update row section displays the purchase fields.](../Media/Screenshots/image261.png "Update row section")

29. Finally, let us add one more step to remove the message from the queue. Press **+New Step**. Type in Queue in the search box, and select Azure Queues -- Delete message.

    ![In the Choose an action section, queue is typed in the search field. Under Services, Azure Queues is selected. On the Actions tab, Azure Queues - Delete message is selected. ](../Media/Screenshots/image262.png "Choose an action section")

30. Select the **receiptgenerator** queue from the list.

31. Select **Message Id** **\>** **Pop Receipt** from the list, and select **Save**.

    ![In the Update row section, on the left in the Delete message fields, Message ID and Pop receipt are selected. On the right, under When there are messages in a queue, Message ID and Pop receipt are selected.](../Media/Screenshots/image263.png "Update row section")

32. Select Run on the Logic App Designer, and then run the Contoso sports Web App and check out an Item.

33. Run the call center website app, and select the last Details link in the list.
    ![Screenshot of the Details link.](../Media/Screenshots/image264.png "Details link")

34. You should now see a Download receipt link because the database has been updated.

    ![In the Order Details window, the Download receipt link is circled.](../Media/Screenshots/image265.png "Order Details window")

35. Select the Download receipt link to see the receipt.

36. Return to the Logic app and you should see all green check marks for each step. If not, select the yellow status icon to find out details.

    ![In the Logic app, all steps have green checkmarks.](../Media/Screenshots/image267.png "Logic app")

### Task 3: Use Twilio to send SMS Order Notifications

#### Subtask 1: Configure your Twilio trial account

1. If you do not have a Twilio account, sign up for one for free at the following URL:

    [**https://www.twilio.com/try-twilio**](https://www.twilio.com/try-twilio)

    ![Screenshot of the Twilio account Sign up for free webpage.](../Media/Screenshots/image268.png "Twilio account Sign up webpage")

2. Select **All Products & Services**.

    ![In the Console, under Home, the All Products and Services (ellipses) button is selected.](../Media/Screenshots/image270.png "Console")

3. Select **Phone Numbers**.

    ![On the Console, under Numbers, Phone Numbers is selected.](../Media/Screenshots/image271.png "Console")

4. Select **Get Started**.

    ![On the Console, under Phone Numbers, the Get Started button is selected.](../Media/Screenshots/image272.png "Console")

5. Select the **Get your first Twilio phone number** button.

    ![On the Get Started with Phone Numbers prompt, the Get your first Twilio phone number button displays.](../Media/Screenshots/image273.png "Get Started with Phone Numbers prompt")

6. Record the **Phone Number**, select the **Choose this Number** button on the **Your first Twilio Phone Number** prompt, and select **Done**.

    ![On the Your first Twilio Phone Number prompt, the number is obscured.](../Media/Screenshots/image274.png "Your first Twilio Phone Number prompt")

7. Select **Home**, then **Settings**. Authenticate if needed and then record the **Account SID** and **Auth Token** for use when configuring the Twilio Connector.

    ![On the Console, on the left, the Home button and the Settings menu tab are selected. On the right, under API Credentials, Account SID and Auth Token are circled.](../Media/Screenshots/image275.png "Console")

#### Subtask 2: Create a new logic app

1. Open **SQL Server Management Studio** and connect to the SQL Database for the **ContosoSportsDB** database.

    >**Note**: You can find the database server name by:
    > - Navigate the Azure ContosoSportsDB in the portal.
    > - In the Overview, locate the **Show database connection strings** link.
    > - Copy the **Server** parameter value.
    e.g. Server=tcp:``contososqlserver2019th.database.windows.net,1433``

    ![In Object Explorer, ContosoSportsDBserver1234.database is selected.](../Media/Screenshots/image276.png "Object Explorer")

2. Under the **ContosoSportsDB** database, expand **Programmability**, right-click on **Stored Procedures**, select **New**, followed by **Stored Procedure...**

    ![In Object Explorer, the following path is expanded: ContosoSportsDBserver1234.database\\Databases\\ContosoSportsDB\\Programmability\\Stored Procedures. From the right-click menu for the Stored Procedures, New / Stored Procedure is selected.](../Media/Screenshots/image277.png "Object Explorer")

3. Replace the Stored Procedure Template code with the following:

    ```sql
    CREATE PROCEDURE [dbo].[GetUnprocessedOrders]
    AS
    declare @returnCode int 
    SELECT @returnCode = COUNT(*) FROM [dbo].[Orders] WHERE PaymentTransactionId is not null AND PaymentTransactionId <> '' AND Phone is not null AND Phone <> '' AND SMSOptIn = '1' AND SMSStatus is null
    return @returnCode

    GO
    ```

4. Select **Execute** in the toolbar, or press the F5 key.

    ![Screenshot of the Execute button.](../Media/Screenshots/image278.png "Execute button")

5. Delete the SQL script for the Stored Procedure from the code editor, and replace it with the following:

    ```sql
    CREATE PROCEDURE [dbo].[ProcessOrders]
    AS
    SELECT * FROM [dbo].[Orders] WHERE PaymentTransactionId is not null AND PaymentTransactionId <> '' AND Phone is not null AND Phone <> '' AND SMSOptIn = '1' AND SMSStatus is null;

    UPDATE [dbo].[Orders] SET SMSStatus = 'sent' WHERE PaymentTransactionId is not null AND PaymentTransactionId <> '' AND Phone is not null AND Phone <> '' AND SMSOptIn = '1' AND SMSStatus is null;
    ```

6. Select **Execute** in the toolbar, or press the F5 key.

    ![Screenshot of the Execute button.](../Media/Screenshots/image278.png "Execute button")

7. In the Azure Management Portal, select the **+Create a resource** button, then **Web**, and, finally **Logic App**.

    ![In the Azure Portal, on the left side, the Create a resource menu option is selected. On the right, Web and Logic App are selected.](../Media/Screenshots/image279.png "Azure Portal")

8. On the **Create logic app** blade, assign a value for **Name**, and set the Resource Group to **contososports**.

    ![In the Create logic app blade, the Name field is set to contososportssms. Under Resource group, Use existing is selected, and contososports is selected.](../Media/Screenshots/image280.png "Create logic app blade")

9. In the navigation menu to the left in the Portal, select **Resource Groups** then **contososports**, then the new Logic App you just created. 

10. In the Logic App blade, under the **DEVELOPMENT TOOLS** menu area, select **Logic App Designer**. Then, select the **Blank Logic App** Template.

    ![In the Logic Apps Designer, the Blank Logic App tile is selected.](../Media/Screenshots/image282.png "Logic Apps Designer")

11. On the **Logic Apps Designer**, select **Schedule**. Then, select **Schedule - Recurrence**.

    ![In the Logic Apps Designer, the Schedule tile is selected.](../Media/Screenshots/image283.png "Logic Apps Designer")

12. Set the **FREQUENCY** to **MINUTE**, and **INTERVAL** to 1.

    ![Under Recurrence, the Frequency field is Minute, and the Interval field is 1.](../Media/Screenshots/image284.png "Recurrence section")

13. Select the **New Step** button followed by **Add an action**.

    ![The New step button and Add an action buttons are selected.](../Media/Screenshots/image285.png "Recurrence section")

14. Type **SQL Server** into the filter box, and select the SQL **Server -- Execute stored procedure** action.

    ![Under Choose an action, sql server is typed in the search field. On the Actions tab, SQL Server (Execute stored procedure) is selected.](../Media/Screenshots/image286.png "Choose an action section")

15. The first time you add a SQL action, you will be prompted for the connection information. Name the connection **ContosoDB**, input the server and database details used earlier, and select **Create**.

    ![In the SQL Server - Execute stored procedure section, the Connection Name is contosoDB. Server and database details are the same as used earlier.](../Media/Screenshots/image287.png "SQL Server - Execute stored procedure section")

16. Select the **\[dbo\].\[GetUnprocessedOrders\]** stored procedure from the drop-down on the Procedure Name field.

    ![In the Execute stored procedure section, the Procedure name is \[dbo\].\[GetUnprocessedOrders\].](../Media/Screenshots/image288.png "Execute stored procedure section")

17. Select **New Step**, and search for and select the **Control** object.

    ![The Control object is highlighted on the logic app designer pick tool.](../Media/Screenshots/image289.png "Buttons")

18. Select **New Step**, and search for and select the **Control -> Condition** object.

    ![The Control Condition object is highlighted on the logic app designer pick tool.](../Media/Screenshots/image290b.png "Buttons")  

19. Select **Choose a value**, and then select **Return Code** from the Dynamic content tile.

    ![The Choose a value box and Return Code objects are highlighte in the Dynamic content tile in the Logic Designer.](../Media/Screenshots/image290c.png "Buttons")

20. Specify **ReturnCode**, set the RELATIONSHIP to **is greater than**, and set the VALUE to **0**.

    ![Under Condition, Object Name is ReturnCode, Relationship is greater than, and Value is 0.](../Media/Screenshots/image290.png "Condition section")

21. Select the **Add an action** link on the **If true** condition.

    ![Under If true, the Add an action button is selected.](../Media/Screenshots/image291.png "If yes section")

22. Select **SQL Server**, and then select the **SQL Server -- Execute stored procedure** action

    ![Under If Yes, SQL Server - Execute stored procedure is circled.](../Media/Screenshots/image292.png "If yes section")

23. Select the **ProcessOrders** stored procedure in the Procedure name dropdown.

    ![Under If Yes, Execute stored procedure 2 is selected, and the Procedure name is \[dbo\].\[ProcessOrders\].](../Media/Screenshots/image293.png "If yes section")

24. Select the **Add an action** link.

    ![The Add an action button is selected.](../Media/Screenshots/image294.png "Add an action button")

25. Type **Twilio** in the filter box, and select the **Twilio -- Send Text Message (SMS)** connector.

    ![Under Show Microsoft managed APIs, the Search box is set to Twilio, and below, Twilio - Send Text Message (SMS) is selected.](../Media/Screenshots/image295.png "Show Microsoft managed APIs")

26. Set the Connection Name to Twilio, specify your Twilio **Account SID** and **Authentication Token**, then select the **Create** button.

    ![In the Twilio - Send Text Message (SMS) section, fields are set to the previously defined settings.](../Media/Screenshots/image296.png "Twilio - Send Text Message (SMS)")

27. Using the drop-down, select your Twilio number for the **FROM PHONE NUMBER** field. Specify a place holder phone number in the **TO PHONE NUMBER**, and a **TEXT** message.

    ![Under Send Text Message (SMS), the From Phone Number and To Phone Number fields are circled, and in the Text field is the message, Hello, your order has shipped!](../Media/Screenshots/image297.png "Send Text Message (SMS)")

28. On the Logic App toolbar, select the **Code View** button.

    ![The code view button is selected on the Logic App toolbar.](../Media/Screenshots/image298.png "Logic App toolbar")

29. Find the **Send\_Text\_Message\_(SMS)** action, and modify the body property of the Twilio action:

    ![The Code view displays the text message, and the from and to phone numbers.](../Media/Screenshots/image299.png "Code view")

    Add the following code between Hello and the comma:

    ```json
    "@{item()['FirstName']}"
    ```

    ![The Code view now displays the added code in the text message.](../Media/Screenshots/image300.png "Code view")

30. Modify the **to** property to pull the phone number from the item.

    ```json
    "to": "@{item()['Phone']}"
    ```

    ![The to phone number code now displays the updated line of code.](../Media/Screenshots/image301.png "Code view")

31. Immediately before the **Send\_Text\_Message\_(SMS)** section, create a new line, and add the following code:

  ```json
    "forEach_email": {
      "type": "Foreach",
      "foreach": "@body('Execute_stored_procedure_(V2)_2')['ResultSets']['Table1']",
      "actions": {
  ```

32. Remove the **runAfter** block from the **Send\_Text\_Message\_(SMS)** action.

    ![The runAfter block of code displays.](../Media/Screenshots/image302.png "Code view")

33. Locate the closing bracket of the **Send\_Text\_Message\_(SMS)** action, create a new line after it (be **SURE** to place a leading comma after the closing bracket), and add the following code:

  ```json
        },
        "runAfter": {
            "Execute_stored_procedure_(V2)_2": [
                "Succeeded"
            ]
        }
    }
  ```

34. Select **Save** on the toolbar to enable the logic app.

    ![On the Logic Apps Designer toolbar, the Save button is selected.](../Media/Screenshots/image304.png "Logic Apps Designer toolbar")

35. After the code for the **Send\_Text\_Message\_(SMS)** has been modified to be contained within the **forEach\_email** action and you save it, it should look like the following:

    ![The Code view displays the code from \"Foreach\" to \"Execute stored procedure.\"](../Media/Screenshots/image303.png "Code view")

36. Your workflow should look like below, and you should receive a text for each order you have placed.

    ![The Workflow diagram begins with Recurrence, then flows to Execute stored procedure, then to Condition. The Condition fields are as follows: Object Name, ReturnCode; Relationship, is greater than; Value, 0. Below the Workflow diagram is an If Yes box, with a workflow that begins wtih Execute stored procedure 2, and flows to forEach email. There is also an If No, Do Nothing box.](../Media/Screenshots/image305.png "Workflow diagram")

![Kabel](../Media/Kabel.png "https://www.kabel.es")