# checkout

This is a very basic (not finished) implementation of a checkout and a pricing controller along with the model for stock keeping units and discount rules. It is currently missing any shape of UI but can be tested through Unit Tests.

The Checkout Controller maintains a basket or a collection of the scanned items. It does not clear that basket at the moment nor does it implement sessions. 
The assumption is that the Checkout controller will live on the actual machine that scans the items while the Pricing controller will be used as a service and installed on a different host. 

If the Checkout module is to be installed on a website, then it will need to implement caching for the shopping cart on the server side (and the client side).

The Pricing Controller is meant to apply the discount rules suitable for a basket. However, it doesn't yet look for the best combination of discount rules that can be applied. 








