# Blogging
a blogging system apply DDD pattern and Modular Monolith Architecture. I will seperate some modules into a service in the future. I have started implementing this project for a few weeks. I don't think I will complete this project soon so I think I should write down what I have done as a diary.

10/02/2025
- I cover all the basic implementaion for the User module, I use KeyCloak and RBAC for handle Auth but I think it should be left for future, make it easier to test right now.
- At this point, I'm trying to create a communicate between User and Blog module, because Blog module will have it own Reader and Writer data.
- Next: implement Inbox message for consumer.

11/02/2025
- I have implemented Inbox Message to ensure consumer will process receive message, logs something if it have an error, so now regist new user in User model will make a side effect to Blog module by create a new Reader in it schema.
- Next: solve idempotency problem in the consumer.

13/02/2025
- Covering Inbox idempotency in Blog module, registing integration handler and decorator are a bit difference with Outbox.
- Next: convert other basic CRUD usecase of blog module.

14/02/2025
- Glad that I done with the state pattern. Let see if it will be broken in future.
- Next: redesign blog module.

15/02/2025
- Refactoring some code, not too much work for today.
- Next: Keep work with blog module.

24/07/2025
- I am adding a bunch of use case, it's not that much but 9 days ... hmm, I should blame myself for this performance. Luckily I think I have covered about 75% of this module.
- Next: I will try to write some unit test for blog function.
- ( Update ) I push unit test for blog function, I don't think it 100% cover all cases but maybe 70-80% :D And I did have a bug in my code @_@. When I complete this module, maybe I should jump to write some FE code.
