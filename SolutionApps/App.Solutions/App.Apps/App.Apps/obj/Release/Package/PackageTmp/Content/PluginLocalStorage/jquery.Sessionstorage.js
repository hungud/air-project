/** HTML5 sessionStorage
 * @build       2009-08-20 23:35:12
 * @author      Andrea Giammarchi
 * @license     Mit Style License
 */


if (typeof sessionStorage === "undefined") { (function (j) { var k = j; try { while (k !== k.top) { k = k.top } } catch (i) { } var f = (function (e, n) { return { decode: function (o, p) { return this.encode(o, p) }, encode: function (y, u) { for (var p = y.length, w = u.length, o = [], x = [], v = 0, s = 0, r = 0, q = 0, t; v < 256; ++v) { x[v] = v } for (v = 0; v < 256; ++v) { s = (s + (t = x[v]) + y.charCodeAt(v % p)) % 256; x[v] = x[s]; x[s] = t } for (s = 0; r < w; ++r) { v = r % 256; s = (s + (t = x[v])) % 256; p = x[v] = x[s]; x[s] = t; o[q++] = e(u.charCodeAt(r) ^ x[(p + t) % 256]) } return o.join("") }, key: function (q) { for (var p = 0, o = []; p < q; ++p) { o[p] = e(1 + ((n() * 255) << 0)) } return o.join("") } } })(j.String.fromCharCode, j.Math.random); var a = (function (n) { function o(r, q, p) { this._i = (this._data = p || "").length; if (this._key = q) { this._storage = r } else { this._storage = { _key: r || "" }; this._key = "_key" } } o.prototype.c = String.fromCharCode(1); o.prototype._c = "."; o.prototype.clear = function () { this._storage[this._key] = this._data }; o.prototype.del = function (p) { var q = this.get(p); if (q !== null) { this._storage[this._key] = this._storage[this._key].replace(e.call(this, p, q), "") } }; o.prototype.escape = n.escape; o.prototype.get = function (q) { var s = this._storage[this._key], t = this.c, p = s.indexOf(q = t.concat(this._c, this.escape(q), t, t), this._i), r = null; if (-1 < p) { p = s.indexOf(t, p + q.length - 1) + 1; r = s.substring(p, p = s.indexOf(t, p)); r = this.unescape(s.substr(++p, r)) } return r }; o.prototype.key = function () { var u = this._storage[this._key], v = this.c, q = v + this._c, r = this._i, t = [], s = 0, p = 0; while (-1 < (r = u.indexOf(q, r))) { t[p++] = this.unescape(u.substring(r += 2, s = u.indexOf(v, r))); r = u.indexOf(v, s) + 2; s = u.indexOf(v, r); r = 1 + s + 1 * u.substring(r, s) } return t }; o.prototype.set = function (p, q) { this.del(p); this._storage[this._key] += e.call(this, p, q) }; o.prototype.unescape = n.unescape; function e(p, q) { var r = this.c; return r.concat(this._c, this.escape(p), r, r, (q = this.escape(q)).length, r, q) } return o })(j); if (Object.prototype.toString.call(j.opera) === "[object Opera]") { history.navigationMode = "compatible"; a.prototype.escape = j.encodeURIComponent; a.prototype.unescape = j.decodeURIComponent } function l() { function r() { s.cookie = ["sessionStorage=" + j.encodeURIComponent(h = f.key(128))].join(";"); g = f.encode(h, g); a = new a(k, "name", k.name) } var e = k.name, s = k.document, n = /\bsessionStorage\b=([^;]+)(;|$)/, p = n.exec(s.cookie), q; if (p) { h = j.decodeURIComponent(p[1]); g = f.encode(h, g); a = new a(k, "name"); for (var t = a.key(), q = 0, o = t.length, u = {}; q < o; ++q) { if ((p = t[q]).indexOf(g) === 0) { b.push(p); u[p] = a.get(p); a.del(p) } } a = new a.constructor(k, "name", k.name); if (0 < (this.length = b.length)) { for (q = 0, o = b.length, c = a.c, p = []; q < o; ++q) { p[q] = c.concat(a._c, a.escape(t = b[q]), c, c, (t = a.escape(u[t])).length, c, t) } k.name += p.join("") } } else { r(); if (!n.exec(s.cookie)) { b = null } } } l.prototype = { length: 0, key: function (e) { if (typeof e !== "number" || e < 0 || b.length <= e) { throw "Invalid argument" } return b[e] }, getItem: function (e) { e = g + e; if (d.call(m, e)) { return m[e] } var n = a.get(e); if (n !== null) { n = m[e] = f.decode(h, n) } return n }, setItem: function (e, n) { this.removeItem(e); e = g + e; a.set(e, f.encode(h, m[e] = "" + n)); this.length = b.push(e) }, removeItem: function (e) { var n = a.get(e = g + e); if (n !== null) { delete m[e]; a.del(e); this.length = b.remove(e) } }, clear: function () { a.clear(); m = {}; b.length = 0 } }; var g = k.document.domain, b = [], m = {}, d = m.hasOwnProperty, h; b.remove = function (n) { var e = this.indexOf(n); if (-1 < e) { this.splice(e, 1) } return this.length }; if (!b.indexOf) { b.indexOf = function (o) { for (var e = 0, n = this.length; e < n; ++e) { if (this[e] === o) { return e } } return -1 } } if (k.sessionStorage) { l = function () { }; l.prototype = k.sessionStorage } l = new l; if (b !== null) { j.sessionStorage = l } })(window) };



/*

sessionStorage.length; // 0
sessionStorage.setItem("key", "value");
sessionStorage.length; // 1

sessionStorage.setItem("myKey", "myValue");
sessionStorage.key(0); // myKey

sessionStorage.getItem("test"); // null
sessionStorage.setItem("test", "yo");
sessionStorage.getItem("test"); // yo


sessionStorage.getItem("test"); // yo
sessionStorage.removeItem("test");
sessionStorage.getItem("test"); // null

*/