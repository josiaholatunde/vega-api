import { Contact } from './Contact';

export interface SaveVehicle {
  id: number;
  makeId: number;
  modelId: number;
  isRegistered: boolean;
  contact: Contact;
  vehicleFeatures: number[];
}
