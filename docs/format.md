## Folder structure

A ZIP file's contents must be exactly like the folder, with no subdirectories. The folder must contain at least one `lisp` file describing the doll itself, and at least one `png` file for the doll to use, plus anything else. Very simple, then.

## S-Expression structure

A doll configuration file is a single [S-expression](https://en.wikipedia.org/wiki/S-expression), containing at least a `cells` form, Other supported forms are `screen`, `events` and **TBD**.

The `cells` form takes a list of `file` forms with the following items:

* `file`: the base name of the cell's image. The `.png` extension is implied.
* `id`: used to build multi-cell objects and for scripting. If missing, `file` is used as an identifier instead.
* `partof`: the ID of another cell, used to build multi-cell objects.
* `pos`: a two-value list to specify the starting position. (Consider: a list of lists, one item per set. If an item is `false` instead of a position, un-map it from that set. If it's a single position, use it through all sets.)
* `offset`: a two-value list to specify an extra offset for this cell as part of a multi-cell object, so you can crop your PNG files.
* `fix`: a value determining how many attempts are needed before the object comes off. A value of 9999 or higher makes it permanently stuck.
* `locked`: a clearer alternative to `fix 9999` that does not take any arguments.
* `alpha`: a value from 0 to 255 to determine the opacity of the cell. PNG already allows this but this lets you *change* the cell's opacity on the fly.

The `events` form likewise takes a list, this time of event structures. Each event starts with an *event trigger* form, followed by any number of *command* forms.

An example:

```clojure
(
	(screen 640 400) ;sets the playfield size
    (background 100 149 237) ;sets the background color
    ;(background gradient 100 149 237 67 100 160 45) ;use a gradient at a 45-degree angle as the background
	(cells
		(file "shorts" offset (0 2) pos (240 94))
		(file "shirt" offset (0 2) pos (240 16))
		(file "suit" id "bathing suit" pos ((240 140) (260 160) (280 180)) on "012")
		(file "leftleg" pos (200 128))
		(file "rightleg" pos (160 128))
		(file "bottom" pos (73 131) fix 4)
		(file "top" pos (61 92) fix 4)
		(file "base" pos (4 4) locked)
		(file "shorts_" partof "shorts" offset (6 0))
		(file "shirt_" partof "shirt" offset (27 0))
	)
	(events
		((initialize)
		)
		((collide "bathing suit" "base")
			(moverel "bathing suit" "base" 57 66)
        )
		((collide "shirt" "base") (moverel "shirt" "base" 54 59))
		((collide "shorts" "base") (moverel "shorts" "base" 66 121))
		((collide "top" "base") (moverel "top" "base" 57 88))
		((collide "bottom" "base") (moverel "bottom" "base" 69 127))
		((collide "rightleg" "base") (moverel "rightleg" "base" 25 140))
		((in "leftleg" "base") (moverel "leftleg" "base" 54 231))
	)
)

```

Given matching cell images the above configuration may look a little like this:

![](lumsample.png)

## Scripting

Scripting is inspired by the [FKiSS standard](http://tigger.orpheusweb.co.uk/KISS/fkref4.html), reworked into a Lisp style. At this time, only the `initialize`, `collide`, `in`, and `alarm` events are supported, and only `timer` and `moverel` commands.

### Event triggers

#### `(initialize)`

Triggered right after loading the doll.

#### `(alarm <number, string, or symbol>)`

Triggered when the specified timer runs out. 

#### `(collide <object> <object>)`

Triggered when the user releases an object such that it touches another, pixel-wise. In FKiSS 2.1 this would only work on individual cells, and in FKiSS 4 it can be a cell, object, or group.

#### `(in <object> <object>)`

The same as `collide`, but rectangular instead of pixel-wise. In FKiSS 2.1, this would only work on objects, in FKiSS 4 it can be a cell, object, or group.

### Commands

#### `(timer <delay> <number, string, symbol, or expression list> [repeat]>`

Sets a timer to go off after `delay`. If the second parameter is a number, string, or symbol, a matching `alarm` event is triggered. If it's an expression list, its contents will serve as the `alarm` event. If `repeat` is given, the timer will trigger continuously in `delay` intervals.

```clojure
; Every 25 ticks, move object "one" two pixels to the right of "other" using an anonymous callback instead of an alarm.
(timer 25
	(
		(moverel "one" "other" 2 0)
        ; Could do more things.
	)
	repeat
)
```

#### `(moverel <object> <object> <int> <int>)`

Basically a shorthand for FKiSS' `movebyx`/`movebyy` commands. 



