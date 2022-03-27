import {GameDataServiceInterface} from "./game-data-service.interface";
import {Observable} from "rxjs";
import {SupportedComponentsModel} from "../model/supported-components.model";

export class SynthRidersGameDataService implements GameDataServiceInterface
{
  private host = "localhost";
  private port = 9000;

  private websocket$: WebSocket;

  constructor(options: any) {

  }
  connect(options: any): Observable<any> {
    return undefined;
  }

  getName(): string {
    return "";
  }

  supports(): SupportedComponentsModel {
    return undefined;
  }

}
