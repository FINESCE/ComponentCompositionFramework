# component-composition-framework

## Preface

Within this document you find a self-contained open specification of the aforementioned Domain Specific Enabler (DSE).

## Terms and Conditions

Software associated to the CCF is provided as open source under the Apache2 license

## Overview

### What you get

The CCF is a plugin-based framework (developed in .NET, C#) that allows you to inject your own implemented components into a composition service that manages and connects all running components dynamically.

In addition the CCF comes with an NGSI9 producer component that any other component can hook itself up against, allowing the CCF to send NGSI9 context updates without requiring the developer knowing the NGSI9 protocol.

### Why to get it

A quick way to make existing components NGSI9 enabled using the already implemented NGSI9-producer component.

### Basic Design Principles

Components implementing IComponent is managed and connected by the Composition Service as appropriate.

## Detailed Specifications

In order to implement a Component, one must implement the IComponent interface:

![IComponent Image](https://raw.githubusercontent.com/insero-software/component-composition-framework/master/Documentation/IComponent.png)

The starting point for any Component is the “Start” method, which is called by the Composition Service when it is determined the Component should be started. The Name and Description properties are strictly for debugging purposes.
 
If the IComponent must allow other components to communicate with it, it can implement the IConnectableComponent interface:

![IConnectable Component Imagee](https://raw.githubusercontent.com/insero-software/component-composition-framework/master/Documentation/IConnectableComponent.png)

Notice that the IComponentCommunication interface does not require any implementation. It is up to the user to define the methods on an interface that extends from this, that can be exposed to other IComponents.

Finally, we have an interface that allows for a components to connect to another components. This interface is called IConntingComponent<TComponentCommunication> and can be seen below:

![IConnecting Component Imagee](https://raw.githubusercontent.com/insero-software/component-composition-framework/master/Documentation/IConnectableComponent.png)

This is a generic interface of the IComponentCommunication implementation the IComponent must connect to.

NOTE: The NGSI9-producer component that comes with the CCF already implements the IConnectableComponent interface.

The Composition Service will manage all registered IComponent implementations and connect them as appropriate.

## Re-utilised Technologies/Specifications

Communicates NGSI9 with the publish subscriber context broker and has been tested with the Orion Context Broker.

## Instances

An instance of this DSE’s reference implementation runs as a part of the trial and is not publicly accessible.

## Glossary

* GE     Generic Enabler
* NGSI   Next Generation Service Interfaces 
* DSE    Domain Specific Enabler
* CCF    Component Composition Framework

## References

NGSI FIWARE (https://forge.fiware.org/plugins/mediawiki/wiki/fiware/index.php/FI-WARE_NGSI_Open_RESTful_API_Specification)

## Contact Person

 * [Troels Lund Rasmussen](http://insero.com/en/about-us/contact/employees-and-management-team/troels-lund-rasmussen/)
 * [Mikael Guldborg](http://insero.com/en/about-us/contact/employees-and-management-team/mikael-guldborg-rask-andersen/)
