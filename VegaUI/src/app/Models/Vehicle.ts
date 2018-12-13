import { Contact } from './Contact';
import { KeyValuePairResource } from './KeyValuePairResource';

export interface Vehicle {
  id: number;
  model: KeyValuePairResource;
  make: KeyValuePairResource;
  isRegistered: boolean;
  lastUpdated: Date;
  contact: Contact;
  vehicleFeatures: KeyValuePairResource[];
}
