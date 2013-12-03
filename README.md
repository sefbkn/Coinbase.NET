~~~~~~ 1JhFNdHBKnFfSwDiiXxbEocLVz3iVopX2D ~~~~~~~


Coinbase API for .NET

This library is currently in alpha stage.
At the moment, only a few basic subsets
of the Coinbase API are supported, including:

* Prices (buy, sell, spot_rate)
* Sells (sells)
* Buys (buys)
* Account (balance, receive_address, get_new_receive_address)

Code is split into their respective sub-apis
as defined on the coinbase API documentation
website.

Use at your own risk.

----------------------
Basic usage examples can be found in the test
folder (Currently ./Coinbase.Net.Tests/*.cs)
An appropriate API key must be entered in the
test folder's app.config for it to work properly.
