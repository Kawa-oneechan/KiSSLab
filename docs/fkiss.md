*Checked* items are implemented, though some may be implemented in completely different ways. ~Struck~ items will not be implemented.

## Events
### Level 1
- [x] `alarm(n)` - A timer reaches zero.
- [x] `begin()` - This event is triggered *after* the initialize event and *before* the version event.
- [x] `catch(oc)` - The user clicks on the object or cel. Applies to all cels & objects except those with a maximal fix value.
- [x] `col(n)` - The user changes the palette to that specified.
- [x] `drop(oc)` - The user releases the mouse on the object or cel. Applies only to all cels & objects except those with a maximal fix value.
- [ ] `end` - The user quits the player or closes the doll.
- [x] `fixcatch(oc)` - The user clicks on the object or cel. Applies only to fixed cels & objects
- [x] `fixdrop(oc)` - The user releases the mouse on the object or cel.
Applies only to fixed cels & objects
- [x] `initialize` - Before the doll is displayed, after loading.
- [ ] ~never~
- [ ] ~`never`~ - This event is never triggered. Intended for debugging purposes during doll development.
- [x] `press(oc)` - The user clicks on the object or cel. Applies to all cels & objects.
- [x] `release(oc)` - The user releases the object or cel. Applies to all cels & objects.
- [x] `set(n)` - The user changes the set to that specified
- [x] `unfix(oc)` - A previously-fixed cel or object becomes free to move.
### Level 2
- [ ] `apart(c,c)` - The two cels, groups or objects do not overlap, taking transparent pixels into account. Triggers only if the cels did overlap before one of them was moved by the user.
- [X] `collide(c,c)` - The two cels, groups or objects touch, taking transparent pixels into account. Triggers only if the cels did not overlap before one of them was moved by the user.
- [X] `in(oc,oc)` - The two objects, cels or groups overlap, ignoring transparency. Triggers only if the objects did not overlap before one of them was moved by the user.
- [ ] `out(oc,oc)` - The two cels, groups or objects do not overlap, ignoring transparency. Triggers only if the objects did overlap before one of them was moved by the user.
- [ ] `stillin(oc,oc)` - The two objects, cels or groups overlap, ignoring transparency. Triggers irrespective of the state of the two objects before movement
- [ ] `stillout(oc,oc)` - The two objects, cels or groups do not overlap, ignoring transparency. Triggers irrespective of the state of the two objects before movement
- [ ] ~`version(n)`~ - After `begin`, but only if the version code is supported by the current program.
### Level 3
- [ ] ~`label(i)`~ - Not really an event, but a way of sharing common actions. The handler is triggered by a `goto` or `gosub` action from another handler
- [ ] ~`overflow`~ - Triggered when an FKISS3 expression evaluation causes an error, eg division by zero.
### Level 4
- [ ] ~`detached(o)`~ - The object is no longer attached to its parent object. This may be triggered eitherby the FKISS action `detach` or if the object is detached from its parent by a user drag.
- [X] `keypress(k)` - The user has pressed the specified key. This event is triggered once when the key is pressed - there is no autorepeat.
- [X] `keyrelease(k)` - The user has released the specified key (i.e. is no longer pressing it).
- [X] `mousein(og)` - Triggered when the mouse pointer moves over the cel, object or group.
The event is only triggered if the cel, object or group is not occluded by other cels (i.e. if a mouse click would invoke a `press` event).
- [X] `mouseout(og)` - Triggered when the mouse pointer moves away from the cel, object or group.

## Actions
### Level 1
- [x] `altmap(oc)` - If the object/cel is mapped (ie part of the image) unmap it; if it is unmapped (ie not map of the image) map it. This applies in all sets.
- [x] `changecol(d)` - Change the current palette group to that specified.
- [x] `changeset(d)` - Change the current set to that specified.
- [ ] ~`debug(s)`~ - Display the text to the user.
- [x] `map(oc)` - Makes oc part of the current image.
- [x] `move(o,d1,d2)` - Moves o (d1,d2) pixels relative to its current position.
- [ ] ~`nop`~ - Does nothing.
- [ ] ~`quit`~ - Exit the player.
- [x] `randomtimer(i,d2,d3)` - Sets timer d1 to expire in a random time in the range d2 to (d2+d3).
- [x] `sound(f)` - Plays the wav or au file f.
- [ ] ~`shell(s)`~ - Send 's' to the local OS as a CLI command.
- [x] `timer(i,d)` - Sets timer d1 to expire (i.e. trigger the associated alarm event) after d2 milliseconds, unless d2 is zero. If d2 is zero, the timer is cancelled without triggering the alarm.
- [x] `transparent(oc,d)` - Changes the transparency of co by d. The transparency of a cel is between 0 (the cel is as rendered exactly as the cel file) and 255 (completely transparent).
- [x] `unmap(oc)` - Removes oc from the current image.
- [ ] ~`viewport(d1,d2)`~ - Sets the top left of the visible area of the image to (d1,d2).
- [ ] ~`windowsize(d1,d2)`~ - Sets the visible size of the image to (d1,d2).
### Level 2
- [X] `iffixed(o,d1,d2)` - If o is fixed (ie not freely movable), set timer d1 to trigger after time d2.
- [X] `ifmapped(c,d1,d2)` - If c is mapped, set timer d1 to trigger after time d2.
- [ ] `ifmoved(o,d1,d2)` - If o has moved from its original position (in the current set), set timer d1 to trigger after time d2.
- [X] `ifnotfixed(o,d1,d2)` - 	If o is not fixed, set timer d1 to trigger after time d2.
- [X] `ifnotmapped(c,d1,d2)` - If c is not mapped, set timer d1 to trigger after time d2.
- [ ] `ifnotmoved(c,d1,d2)` - If o has not moved from its original location, set timer d1 to trigger after time d2.
- [X] `movebyx(o1,o2,d)` - Sets the x position of o1 to the x position of o2 plus d.
- [X] `movebyy(o1,o2,d)` - Sets the y position of o1 to the y position of o2 plus d.
- [X] `moverandx(o,d1,d2)` - Moves object o horizontally by a random distance in the range d1 to d2.
- [X] `moverandy(o,d1,d2)` - Moves object o vertically by a random distance in the range d1 to d2.
- [X] `moveto(o,d1,d2)` - Moves o to (d1,d2) absolute position.
- [X] `movetorand(o)` - Randomises the position of o.
- [X] `music(f)` - 	Plays the midi file f.
- [X] `notify(s)` - Display the text to the user.
- [X] `setfix(o,d)` - Set the fix attribute of o to d.
### Level 3
- [X] `add(v,d1,d2)` - `v = d1 + d2`
- [X] `div(v,d1,d2)` - `v = d1 / d2`
- [X] `else` - Part of a structured `if`/`else`/`endif`
- [X] `endif` - Terminator for structured `if`/`else`/`endif`
- [ ] ~`exitevent`~ - Aborts the current event handler.
- [ ] `ghost(oc,d)` - If d is not zero, oc becomes a "ghost", otherwise it is unghosted. A ghost object or cel cannot be dragged with the mouse - mouse clicks will be passed down to the next cel below.
- [ ] ~`gosub(d)`~ - Triggers the event handler for label d. When the label event finishes, processing of the current handler will continue.
- [ ] ~`gosubrandom(d1,d2,d3)`~ - Performs a gosub to d2 or d3. d1 is the percentage chance that d2 is chosen rather than d3.
- [ ] ~`goto(d)`~ - Triggers the event handler for label d. When the label event finishes, processing of the current handler will also terminate.
- [X] `ifequal(d1,d2)` - Begins a structured if. The test is true if `d1 = d2`.
- [X] `ifgreaterthan(d1,d2)` - Begins a structured if. The test is true if `d1 > d2`.
- [X] `iflessthan(d1,d2)` - Begins a structured if. The test is true if `d1 < d2`.
- [X] `ifnotequal(d1,d2)` - Begins a structured if. The test is true if `d1 != d2`.
- [X] `let(v,d)` - `v = d`
- [ ] `letcatch(v)` - Sets v to the object number currently being dragged. If no object is being dragged, v is set to -1.
- [X] `letcollide(v,c1,c2)` - If c1 and c2 overlap (taking account of transparency), v is set to 1. Otherwise, v is set to 0.
- [X] `letfix(v,o)` - Sets v to the current fix value of o.
- [X] `letinside(v,o1,o2)` - If o1 and o2 overlap (without taking account of transparency), v is set to 1, otherwise 0.
- [X] `letmapped(v,c)` - Sets v to 1 if c is mapped, otherwise 0.
- [ ] `letmousex(v)` - Sets v to the current mouse x position. If the mouse is outside the playfield, the result is still valid (i.e. the coordinate system extends beyond the playfield).
- [ ] `letmousey(v)` - Sets v to the current mouse y position.
- [X] `letobjectx(v,o)` - Sets v to the x ordinate of o.
- [X] `letobjecty(v,o)` - Sets v to the y ordinate of o.
- [X] `letpal(v)` - Sets v to the currently selected palette group.
- [X] `letset(v)` - Sets v to the currently selected set.
- [X] `lettransparent(v,c)` - If all cels in ovcg have the same transparency, v is set to this value. If cels have different transparencies, v is set to -1.
- [X] `mod(v,d1,d2)` - `v = d1 % d2`.
- [X] `mul(v,d1,d2)` - `v = d1 * d2`.
- [X] `random(v1,d1,d2)` - v1 is set to a random number in the range d1 to d2.
- [X] `sub(v,d1,d2)` - `v = d1 - d2`.
### Level 4
- [ ] ~`attach(o1,o2)`~ - Attach object o1 to object o2. If o1 is already attached to another object, detach it first (triggering a `detached` event if appropriate). After this action, object o2 is the "parent" of object o1, and moving o2 will cause o1 to move so as to retain the same separation. Moving o1 (unless it is fixed) will cause the attachment to br broken.
- [ ] `deletevalue(t)` - Delete the value with tag t from the appropriate pool.
- [ ] ~`detach(o)`~ - Detaches object o from whatever parent it may have had.
- [X] `elseifequal(v1,v2)` - Part of a structured `if`/`elseif`/`else`/`endif`
- [X] `elseifgreaterthan(v1,v2)` - Part of a structured `if`/`elseif`/`else`/`endif`
- [X] `elseiflessthan(v1,v2)` - Part of a structured `if`/`elseif`/`else`/`endif`
- [X] `elseifnotequal(v1,v2)` - Part of a structured `if`/`elseif`/`else`/`endif`
- [ ] ~`exitloop`~ - If the current (label) event has been called, directly or indirectly, by a `repeat` action, abort all event handlers down to that containing the repeat. Action processing will continue with the action after the repeat. If there is no repeat being processed, this action does nothing.
- [ ] ~`glue(o1,o2)`~ - This acts exactly as attach (q.v.), except that moving the child object does not detach it from its parent.
- [ ] ~`letchild(v,ov)`~ - Sets v to the lowest numbered object currently attached to ov. If no objects are attached,v is set to -1.
- [ ] ~`letframe(v,g)`~ - Sets v to the current frame of g. If no setframe has been executed for g, v is set to -1.
- [ ] ~`letheight(v,o)`~ - Sets v to the current height of ov, excluding any unmapped cels.
- [X] `letinitx(v,o)` - Sets v to the initial x ordinate of o (in the current set).
- [X] `letinity(v,o)` - Sets v to the initial y ordinate of o (in the current set).
- [ ] ~`letkcf(v,oc)`~ - Sets v to the palette file number currently being used by ovgc. If ovgc refers to multiple cels that reference more than one palette file, or any cel in ovgc is Cherry KISS, v is set to -1.
- [ ] `letkeys(v,k)` - Sets v to the index of the first key in k that is pressed. Indexing starts at 1.
- [ ] `letkeymap(v,k)` - For each key in k that is pressed, the equivalent bit in v is set.
- [ ] ~`letparent(v,o)`~ - Sets v to the number of the object to which o is currently attached. If o is not attached, v is set to -1.
- [ ] ~`letsibling(v,o)`~ - Sets v to the next lowest numbered object currently attached to the parent of o. If o is not attached, or o is the highest-numbered object attached to its parent, v is set to -1.
- [ ] ~`lettimer(v,t)`~ - Sets v to the number of milliseconds left before the alarm referred to by 'i' times out. If the alarm has timed out, but has not yet been processed, v is set to 0; if the specified alarm does not exist, v is set to -1.
- [ ] ~`letwidth(v,o)`~ - Sets v to the current width of ov, excluding any unmapped cels.
- [ ] `loadvalue(v,t)` - Set v to the value tagged with t stored in the appropriate pool. If tag t does not exist in the pool, v is unchanged.
- [ ] ~`repeat(i,d,v)`~ - Performs a `gosub(i)` 'd' times. Before each gosub, 'v' is set to the index number of the call (i.e. 1 the first time, 2 the second time, etc).
- [ ] `restrictx(ov,d1,d2)` - Restricts all movement (user drags of FKISS "move" functions) such that (the left of) ov remains between d1 and d2. If ov is outside the range when this function is executed, it is moved to the nearest point of the range.
- [ ] `restricty(ov,d1,d2)` - Restricts all movement (user drags of FKISS "move" functions) such that (the top of) ov remains between d1 and d2. If ov is outside the range when this function is executed, it is moved to the nearest point of the range.
- [ ] `savevalue(t,v)` - Store v in the appropriate pool,using the tag t.
- [ ] ~`setframe(g,v)`~ - All cels that are in frame d of group g are mapped, and all cels that are in group g but not in frame d are unmapped.
- [ ] ~`setkcf(oc,i)`~ - The palette file used for all cels in ovgc is changed to that with index 'i'. If i is less than zero or greater than the number of defined palette files, this function will have no effect. If the number of colours in the palette file is insufficient for any cel in ovgc, the result is indeterminate, but will typically result in parts of the cel being rendered in black.
- [ ] `valuepool(t)` - Sets the default pool for all future pool accesses to t. If t is a null string (""), sets the pool to the unique doll-specific pool.

## Types

Usage | Meaning | Description | Examples
------|---------|-------------|---------
c | Cel reference | The name of a cel. | "eyes.cel"
o | Object reference | The mark of an object. | #1
ov | Object (inc variable) | The mark of an object, or a variable containing the object number. | #1, ObjectNumber
oc | Cel or object reference | Either the name of a cel or the mark of an object | #1, ObjectNumber
ovc | Cel or object (inc variable) | Either the name of a cel or the mark of an object, or a variable containing the object number | #4, "shirt.cel"
g | Group | The name of a cel group. | !Body
ocg | Cel, group or object reference | The name of a cel or group, or the mark of an object | "jacket.cel", !Group2, #19
ovgc | Cel, object (inc var) or group | Any of cel name, group, object number, or variable | "fred.cel", #1, !MainGroup, ObjNum
n | Number | A numeric (integer) value | 17
s | String | Any text | "Hello there"
v | Variable | Variable name | n6 (FKISS3), Count (FKISS4)
i | Identifier | Numeric (integer) value, or a symbolic name for a label or an alarm. | GetObj, 23
d | Number or Variable | A literal number or a variable identifier. | 75, k4
f | Filename | String containing a filename. | "bang.wav"
k | Keylist | String containing the names or one or more keyboard keys. In some cases only a single key name is allowed. | "abcd", "updownleftright"
t | Value tag | The tag of a value in a value pool, or the name of a value pool. For most uses, this can be the name of a pool plus the name of a tag, separated by a full stop. | "score-01", "mypool.score"
