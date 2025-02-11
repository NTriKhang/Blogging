# Blogging
a blogging system apply DDD pattern and Modular Monolith Architecture. I will seperate some modules into a service in the future.

10/02/2025, I have started implementing this project for a few weeks. I don't think I will complete this project soon so I think I should write down what I have done as a diary.
- I cover all the basic implementaion for the User module, I use KeyCloak and RBAC for handle Auth but I think it should be left for future, make it easier to test right now.
- At this point, I'm trying to create a communicate between User and Blog module, because Blog module will have it own Reader and Writer data.

11/02/2025
- I have implemented Inbox Message to ensure consumer will process receive message, logs something if it have an error, so now regist new user in User model will make a side effect to Blog module by create a new Reader in it schema.
