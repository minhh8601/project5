import { ApiService } from './../services/api.service';
import { Injector, Renderer2 } from '@angular/core';
import { ActivatedRoute } from '@angular/router';



export class BaseComponent {
  public _renderer: any;
  public _route: ActivatedRoute;
  public _api : ApiService
  constructor(injector: Injector) {
      this._renderer = injector.get(Renderer2);
      this._route = injector.get(ActivatedRoute);
      this._api = injector.get(ApiService);
  }
  public loadScripts(...list: string[] ) {
      list.forEach(x=> {
          this.renderExternalScript(x).onload = () => {
          }
      })
  }
  public renderExternalScript(src: string): HTMLScriptElement {
      const script = document.createElement('script');
      script.type = 'text/javascript';
      script.src = src;
      script.async = true;
      script.defer = true;

      this._renderer.getElementsByTagName('body')[0].appendChild(document.body, script);
      return script;
  }
}
